using AutoMapper;
using G7_Microservices.Backend.ShoppingCartAPI.Data;
using G7_Microservices.Backend.ShoppingCartAPI.Models;
using G7_Microservices.Backend.ShoppingCartAPI.Models.Dto;
using G7_Microservices.Backend.ShoppingCartAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G7_Microservices.Backend.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly ICouponService _couponService;
        private readonly IProductService _productService;
        private readonly IConfiguration _configuration;

        public ShoppingCartAPIController(ApplicationDbContext db,
            IMapper mapper,
            ICouponService couponService,
            IProductService productService,
            IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _couponService = couponService;
            _productService = productService;
            _configuration = configuration;
            _responseDto = new ResponseDto();
        }

        [HttpPost("ApplyCoupon")]
        public ResponseDto ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders
                    .FirstOrDefault(x => x.UserId == cartDto.CartHeaderDto.UserId && !x.IsDeleted);
                if (cartHeaderFromDb != null)
                {
                    cartHeaderFromDb.CouponCode = cartDto?.CartHeaderDto?.CouponCode;
                    _db.CartHeaders.Update(cartHeaderFromDb);
                    _db.SaveChanges();
                }

                _responseDto.Result = true;
                _responseDto.Message = "Cupon aplicado con exito";
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Ocurrio un error: " + ex.Message;
            }

            return _responseDto;
        }

        [HttpPost("RemoveCoupon")]
        public ResponseDto RemoveCoupon([FromBody] ApplyCouponDto applyCouponDto)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders
                    .FirstOrDefault(x => x.UserId == applyCouponDto.UserId && !x.IsDeleted);
                if (cartHeaderFromDb != null)
                {
                    cartHeaderFromDb.CouponCode = "";
                    _db.CartHeaders.Update(cartHeaderFromDb);
                    _db.SaveChanges();
                }

                _responseDto.Result = true;
                _responseDto.Message = "Cupon eliminado con exito";
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Ocurrio un error: " + ex.Message;
            }

            return _responseDto;
        }

        [HttpPost]
        public ResponseDto UpsertCart(CartDto cartDtoRequest)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _db.CartHeaders
                    .FirstOrDefault(x => x.UserId == cartDtoRequest.CartHeaderDto.UserId && !x.IsDeleted);

                #region POST
                CartHeader newCartHeader = new();
                CartDetails newcartDetails = new CartDetails();

                if(cartHeaderFromDb == null)
                {
                    newCartHeader.UserId = cartDtoRequest.CartHeaderDto.UserId;
                    newCartHeader.CouponCode = cartDtoRequest.CartHeaderDto.CouponCode;
                    newCartHeader.Discount = cartDtoRequest.CartHeaderDto.Discount;
                    newCartHeader.CartTotal = cartDtoRequest.CartHeaderDto.CartTotal;

                    _db.CartHeaders.Add(newCartHeader);
                    _db.SaveChanges();

                    //Relacionar el cartHeader con sus CartDetails
                    cartDtoRequest.CartDetailsDtos.First().CartHeaderId = newCartHeader.Id;

                    CartDetailsDto? cartDetailsDto = cartDtoRequest.CartDetailsDtos.First();
                    newcartDetails.CartHeaderId = newCartHeader.Id;
                    newcartDetails.ProductId = cartDetailsDto.ProductId;
                    newcartDetails.Count = cartDetailsDto.Count;

                    _db.CartDetails.Add(newcartDetails);
                    _db.SaveChanges();

                    _responseDto.Result = newCartHeader.Id;
                    _responseDto.Message = "Cart creado con exito";

                }
                #endregion

                #region Update
                else
                {
                    //Revisar si los details tienen el mismo producto
                    CartDetails? cartDetailsFromDb = _db.CartDetails.AsNoTracking().FirstOrDefault(
                        x=>x.ProductId == cartDtoRequest.CartDetailsDtos.First().ProductId &&
                        x.CartHeaderId == cartHeaderFromDb.Id);

                    if(cartDetailsFromDb == null)
                    {
                        CartDetailsDto? cartDetailsDto = cartDtoRequest.CartDetailsDtos.FirstOrDefault();
                        newcartDetails.CartHeaderId = cartDetailsDto.CartHeaderId;
                        newcartDetails.ProductId = cartDetailsDto.ProductId;
                        newcartDetails.Count= cartDetailsDto.Count;

                        _db.CartDetails.Add(newcartDetails);
                        _db.SaveChanges();

                        _responseDto.Result = newCartHeader.Id;
                        _responseDto.Message = "CartDetails agregados con exito";

                    }
                    else
                    {
                        //Si existen los details los actualizamos
                        cartDetailsFromDb.Count += cartDtoRequest.CartDetailsDtos.FirstOrDefault().Count;
                        cartDetailsFromDb.CartHeaderId = cartDtoRequest.CartDetailsDtos.FirstOrDefault().CartHeaderId;
                        cartDetailsFromDb.ProductId = cartDtoRequest.CartDetailsDtos.FirstOrDefault().ProductId;

                        _db.CartDetails.Update(cartDetailsFromDb);
                        _db.SaveChanges();

                        _responseDto.Result = true;
                        _responseDto.Message = "CartDetails actualizados con exito";
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                _responseDto.IsSucess = false;
                _responseDto.Message = "Ocurrio un error: " + ex.Message;
            }

            return _responseDto;
        }

    }
}
