using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Validators;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Services
{
    public class FileApplicationsService
    {
        [Obsolete]
        public Result<string, Notification> UploadImageInDirectory(string base64Image, string FileName = "")
        {
            var validator = new RegisterFileValidator();
            Result<string, Notification> result = validator.ValidateImage(base64Image, FileName);

            if (result.IsFailure)
                return result.Error;

            if (string.IsNullOrWhiteSpace(base64Image))
                return string.Empty;

            if (base64Image.Contains("data:image"))
            {
                int index = base64Image.IndexOf('/') + 1;
                string fileExtension = base64Image[index..base64Image.LastIndexOf(';')];

                FileName += "." + fileExtension;
                base64Image = base64Image[(base64Image.LastIndexOf(',') + 1)..];
            }

            byte[] bytes = Convert.FromBase64String(base64Image);
            string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/img";


            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            string file = Path.Combine(fileDirectory, Guid.NewGuid() + FileName);

            if (bytes.Length > 0)
            {
                using FileStream? stream = new(file, FileMode.Create);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return file;
        }

        [Obsolete]
        public Result<string, Notification> UploadFileinDirectory(string base64File, string FileName = "")
        {
            var validator = new RegisterFileValidator();
            Notification notification = validator.ValidateFile(base64File, FileName);

            if (notification.HasErrors())
                return notification;

            if (string.IsNullOrWhiteSpace(base64File))
                return string.Empty;

            if (base64File.Contains("data:"))
            {
                int index = base64File.IndexOf('/') + 1;
                string fileExtension = base64File[index..base64File.LastIndexOf(';')];

                FileName += "." + fileExtension;
                base64File = base64File[(base64File.LastIndexOf(',') + 1)..];
            }


            byte[] bytes = Convert.FromBase64String(base64File);


            string fileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/img";


            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            string file = Path.Combine(fileDirectory, Guid.NewGuid() + FileName);

            if (bytes.Length > 0)
            {
                using FileStream? stream = new(file, FileMode.Create);
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }

            return file;
        }

        public Result<FileDto, Notification> ConverterBase64InBytes(string base64File, string fileName = "")
        {
            var validator = new RegisterFileValidator();
            Notification notification = validator.ValidateFile(base64File, fileName);

            if (notification.HasErrors())
                return notification;

            if (string.IsNullOrWhiteSpace(base64File))
            {
                notification.AddError(CommonStatic.FileMsgErrorErrorBase64);
            }
            string fileExtension = "";
            string contentType = "";
            if (base64File.Contains("data:"))
            {
                int index = base64File.IndexOf('/') + 1;
                fileExtension = base64File[index..base64File.LastIndexOf(';')];


                int indexContentType = base64File.IndexOf(':') + 1;

                contentType = base64File[indexContentType..base64File.LastIndexOf(';')];


                base64File = base64File[(base64File.LastIndexOf(',') + 1)..];

            }

            var FileName = Guid.NewGuid() + fileName + "." + fileExtension;

            FileDto fileDto = new()
            {
                Bytes = Convert.FromBase64String(base64File),
                FileName = FileName,
                FileExtension = fileExtension,
                ContentType = contentType,
                Url = CommonStatic.Url + CommonStatic.BucketName + "/" + FileName
            };

            return fileDto;
        }

        public void DownloadFileInCloud(FileDto fileDto)
        {
            if (fileDto != null)
            {
                
            }
        }
        //public async void UploadFileInCloudAsync(FileDto fileDto)
        //{
        //    if (fileDto != null)
        //    {
        //        if (fileDto.Bytes != null)
        //        {
        //            var endpoint = CommonStatic.Endpoint;
        //            var accessKey = CommonStatic.AccessKey;
        //            var secretKey = CommonStatic.SecretKey;

        //            var minio = new MinioClient()
        //                                   .WithEndpoint(endpoint)
        //                                   .WithCredentials(accessKey,
        //                                            secretKey)
        //                                   .WithSSL().Build();

        //            var bucketName = CommonStatic.BucketName;
        //            var location = CommonStatic.Location;

        //            BucketExistsArgs bucketExistsArgs = new();
        //            bucketExistsArgs.WithBucket(bucketName);

        //            bool found = await minio.BucketExistsAsync(bucketExistsArgs);

        //            if (!found)
        //            {
        //                MakeBucketArgs makeBucketArgs = new();
        //                makeBucketArgs.WithBucket(bucketName);
        //                makeBucketArgs.WithLocation(location);
        //                await minio.MakeBucketAsync(makeBucketArgs);
        //            }

        //            minio.WithRegion();
        //            var data = new MemoryStream(fileDto.Bytes);
        //            PutObjectArgs putObjectArgs = new();
        //            putObjectArgs.WithBucket(bucketName);
        //            putObjectArgs.WithObject(fileDto.FileName);
        //            putObjectArgs.WithContentType(fileDto.ContentType);
        //            putObjectArgs.WithStreamData(data);
        //            putObjectArgs.WithObjectSize(data.Length);

        //            await minio.PutObjectAsync(putObjectArgs);
        //        }
        //    }
        //}
    }
}
