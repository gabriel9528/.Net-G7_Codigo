using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;
using AnaPrevention.GeneralMasterData.Api.Attachments.Infrastructure.Repositories;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services
{
    public class S3AmazonService(S3AmazonRepository s3AmazonRepository, IConfiguration configuration)
    {
        private readonly S3AmazonRepository _s3AmazonRepository = s3AmazonRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<S3ResponseDto> UploadFileAsync(S3FileDto file, string bucketName)
        {
            try
            {


                var objectS3 = new S3Objects
                {
                    Name = file.FileName,
                    InputStream = file.stream,
                    BucketName = bucketName,
                };


                AwsCredentials credentials;
                if (CommonStatic.IsEnvioromentDev()){
                    credentials = new()
                    {
                        AwsKey = _configuration["AwsConfiguration:AWSAccessKey"] ?? "",
                        AwsSecret = _configuration["AwsConfiguration:AWSSecretKey"] ?? ""
                    };
                }
                else
                {
                    credentials = new()
                    {
                        //AwsKey = Environment.GetEnvironmentVariable("ENV_AWS_KEY") ?? "",
                        //AwsSecret = Environment.GetEnvironmentVariable("ENV_AWS_SECRET") ?? ""
                        AwsKey = _configuration["AwsConfiguration:AWSAccessKey"] ?? "",
                        AwsSecret = _configuration["AwsConfiguration:AWSSecretKey"] ?? ""
                    };
                }
                return await _s3AmazonRepository.UploadFileAsync(objectS3, credentials);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> DownloadObjectS3(S3DownloadFileRequest fileObject)
        {

            try
            {
                var credentials = new AwsCredentials()
                {
                    AwsKey = _configuration["AwsConfiguration:AWSAccessKey"]??"",
                    AwsSecret = _configuration["AwsConfiguration:AWSSecretKey"]??""
                };


                var objectS3 = new S3Objects
                {
                    Name = fileObject.KeyName,
                    BucketName = fileObject.BucketName??"",
                };

                var result = await _s3AmazonRepository.DownloadObjectS3(objectS3, credentials);

                return result ?? "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
