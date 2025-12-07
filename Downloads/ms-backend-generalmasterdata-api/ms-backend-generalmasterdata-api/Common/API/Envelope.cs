using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.API
{
    public class Envelope
    {
        public object? Result { get; }
        public List<Error>? Errors { get; }

        private Envelope(object? result, List<Error>? errors)
        {
            Result = result;
            Errors = errors;
        }

        public static Envelope Ok(object? result = null)
        {
            return new Envelope(result, null);
        }

        public static Envelope BadRequest(List<Error> errors)
        {
            return new Envelope(null, errors);
        }

        public static Envelope NotFound()
        {
            List<Error> errors = new()
            {
                new Error("Not Found")
            };
            return new Envelope(null, errors);
        }

        public static Envelope Forbidden()
        {
            List<Error> errors = new()
            {
                new Error("Forbidden")
            };
            return new Envelope(null, errors);
        }

        public static Envelope Unauthorized()
        {
            List<Error> errors = new()
            {
                new Error("Unauthorized")
            };
            return new Envelope(null, errors);
        }

        public static Envelope ServerError()
        {
            List<Error> errors = new()
            {
                new Error("¡Ups! Parece que algo salió mal en nuestro servidor. Nuestro equipo está trabajando para solucionar el problema. Por favor, inténtalo de nuevo más tarde. Si el problema persiste, no dudes en contactarnos para que podamos ayudarte. - Code 500")
            };
            return new Envelope(null, errors);
        }
    }
}
