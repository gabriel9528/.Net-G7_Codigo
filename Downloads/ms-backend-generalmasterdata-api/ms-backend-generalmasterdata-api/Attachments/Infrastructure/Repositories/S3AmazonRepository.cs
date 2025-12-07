using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;

namespace AnaPrevention.GeneralMasterData.Api.Attachments.Infrastructure.Repositories
{
    public class S3AmazonRepository
    {
        public S3AmazonRepository()
        {
        }

        public async Task<S3ResponseDto> UploadFileAsync(S3Objects s3Obj, AwsCredentials awsCredentials)
        {
            // Addinng credentials of Amazon Bucket S3
            var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecret);

            //  Specify Region Endpoint Amazon Region locate Bucket S3
            var config = new AmazonS3Config()
            {
                //RegionEndpoint = Amazon.RegionEndpoint.USWest2,
                RegionEndpoint = Amazon.RegionEndpoint.USEast1,
            };

            var response = new S3ResponseDto();

            try
            {
                // Create body for request to SDK Amazon S3
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3Obj.InputStream,
                    Key = s3Obj.Name,
                    BucketName = s3Obj.BucketName,
                    CannedACL = S3CannedACL.NoACL,
                    ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256,

                };
                // Create S3 Client
                using var client = new AmazonS3Client(credentials, config);

                //Upload
                var transferUtility = new TransferUtility(client);

                // Actually uploading the object to S3 for Exm(txt, xlsx, html, etc)
                await transferUtility.UploadAsync(uploadRequest);

                //--------------------------    Get Metadata    ---------------------------
                GetObjectMetadataRequest metadataRequest = new()
                {
                    BucketName = s3Obj.BucketName,
                    Key = s3Obj.Name,
                };
                GetObjectMetadataResponse result = await client.GetObjectMetadataAsync(metadataRequest);
                ServerSideEncryptionMethod objectEncryption = result.ServerSideEncryptionMethod;

                //--------------------------    End Metadata    ---------------------------

                response.StatusCode = 200;
                response.Message = s3Obj.Name + " ha sido cargado correctamente";
            }
            catch (AmazonS3Exception ex)
            {
                response.StatusCode = (int)ex.StatusCode;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<string> DownloadObjectS3(S3Objects fileObject, AwsCredentials awsCredentials)
        {
            try
            {
                // Addinng credentials of Amazon Bucket S3
                var credentials = new BasicAWSCredentials(awsCredentials.AwsKey, awsCredentials.AwsSecret);

                //  Specify Region Endpoint Amazon Region locate Bucket S3
                var config = new AmazonS3Config()
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1,
                };

                using var client = new AmazonS3Client(credentials, config);

                //-----------------------------------   Request Object   -----------------------------------

                var request = new GetObjectRequest
                {
                    BucketName = fileObject.BucketName,
                    Key = fileObject.Name,
                };

                using GetObjectResponse response = await client.GetObjectAsync(request);
                using var ms = new MemoryStream();
                await response.ResponseStream.CopyToAsync(ms);
                ms.Position = 0;
                var resultBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(resultBytes, 0, resultBytes.Length);
                return base64String;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
