using AnaPrevention.GeneralMasterData.Api.Common.Application.Settings.Mail.Interfaces;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using CSharpFunctionalExtensions;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Dtos;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail.Interfaces;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Services;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailContents.Applications.Validators;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Infrastructure.Repositories;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos;
using AnaPrevention.GeneralMasterData.Api.Attachments.Domain.Enums;
using AnaPrevention.GeneralMasterData.Api.Attachments.Application.Dtos.S3;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Persons.Domain.Entities;
using AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail.Dto;
using AnaPrevention.GeneralMasterData.Api.Emails.EmailTemplates.Application.Static;

namespace AnaPrevention.GeneralMasterData.Api.Common.Helper.Mail
{
    public class EmailService(IEmailConfiguration emailConfiguration, EmailContentApplicationServices emailContentApplicationServices, AttachmentApplicationService attachmentApplicationService, RegisterEmailContentValidator registerEmailContentValidator, EmailTemplateRepository emailTemplateRepository, S3AmazonService s3bucketService) : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration = emailConfiguration;
        private readonly EmailContentApplicationServices _emailContentService = emailContentApplicationServices;
        private readonly AttachmentApplicationService _attachmentApplicationService = attachmentApplicationService;
        private readonly RegisterEmailContentValidator _registerEmailContentValidator = registerEmailContentValidator;
        private readonly S3AmazonService _s3bucketService = s3bucketService;
        private readonly EmailTemplateRepository _emailTemplateRepository = emailTemplateRepository;

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            throw new NotImplementedException();
        }
       
        public void Send(EmailMessage emailMessage)
        {
            string defaultTypeMedia = "application";
            string defaultSubTypeMedia = "pdf";
            
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.Add(new MailboxAddress(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpUsername));

            message.Subject = emailMessage.Subject;
            //We will say we are sending HTML. But there are options for plaintext etc. 

            // --------- Attachments

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = emailMessage.Content
            };

            foreach (var attachment in emailMessage.Attachments)
            {
                var stream = new MemoryStream(Convert.FromBase64String(attachment.Base64));
                bodyBuilder.Attachments.Add(attachment.Name, stream, ContentType.Parse((attachment.TypeMedia ?? defaultTypeMedia) + "/" + (attachment.SubTypeMedia ?? defaultSubTypeMedia)));
            }

            message.Body = bodyBuilder.ToMessageBody();

            // --------- Attachments

            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using var emailClient = new SmtpClient();
            //The last parameter here is to use SSL (Which you should!)
            //emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

            emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);

            //Remove any OAuth functionality as we won't be using it. 
            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

            emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

            emailClient.Send(message);

            emailClient.Disconnect(true);
        }

        public async Task<List<AttachmentEmailDto>> GetAttachmentById(Guid id, EntityType entityType)
        {
            var listAttchaments = _attachmentApplicationService.GetByEntityId(id, entityType);

            List<AttachmentEmailDto> list = new();

            if (listAttchaments != null)
            {
                foreach (var attchament in listAttchaments)
                {
                    string SubTypeMedia = "";
                    string TypeMedia = "";
                    if (attchament.FileType != FileType.Other)
                    {
                        SubTypeMedia = attchament.FileType.ToString().ToLower();
                        if (attchament.FileType == FileType.PDF)
                            TypeMedia = "application";
                        else
                            TypeMedia = "image";
                    }


                    if (!string.IsNullOrEmpty(SubTypeMedia) && !string.IsNullOrEmpty(TypeMedia))
                    {
                        var s3Object = new S3DownloadFileRequest()
                        {
                            BucketName = CommonStatic.BucketNameFiles,
                            KeyName = attchament.Url,
                        };
                        string attchamentBase64 = await _s3bucketService.DownloadObjectS3(s3Object);
                        if (!string.IsNullOrEmpty(attchamentBase64))
                        {
                            list.Add(new()
                            {
                                Base64 = attchamentBase64,
                                Url = attchament.Url,
                                Name = attchament.Name,
                                SubTypeMedia = SubTypeMedia,
                                TypeMedia = TypeMedia,
                            });
                        }
                    }
                }
            }
            return list;
        }

        public async Task SendAsync(EmailMessage emailMessage, Guid userId,Guid? personId, EmailConfig? emailConfig = null)
        {
            try
            {
                string defaultTypeMedia = "application";
                string defaultSubTypeMedia = "pdf";
                string smtpUsername = "";
                string smtpServer = "";
                string smtpPassoword = "";
                int SmtpPort;
                SecureSocketOptions SecureSocketOptions;
                if (emailConfig != null)
                {
                    smtpUsername = emailConfig.Email;
                    smtpServer = emailConfig.Host;
                    smtpPassoword = emailConfig.Password;
                    SmtpPort = emailConfig.Port;
                    SecureSocketOptions = emailConfig.SecureSocketOptions;
                }
                else
                {
                    smtpUsername = _emailConfiguration.SmtpUsername;
                    smtpServer = _emailConfiguration.SmtpServer;
                    smtpPassoword = _emailConfiguration.SmtpPassword;
                    SmtpPort = _emailConfiguration.SmtpPort;
                    SecureSocketOptions = SecureSocketOptions.StartTls;
                }

                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.Add(new MailboxAddress(smtpServer, smtpUsername));

                message.Subject = emailMessage.Subject;
                //We will say we are sending HTML. But there are options for plaintext etc. 

                // --------- Attachments

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = emailMessage.Content
                };

                if (emailMessage.Attachments != null)
                {
                    foreach (var attachment in emailMessage.Attachments)
                    {
                        var stream = new MemoryStream(Convert.FromBase64String(attachment.Base64));
                        bodyBuilder.Attachments.Add(attachment.Name, stream, ContentType.Parse((attachment.TypeMedia ?? defaultTypeMedia) + "/" + (attachment.SubTypeMedia ?? defaultSubTypeMedia)));
                    }
                }

                message.Body = bodyBuilder.ToMessageBody();

                // --------- Attachments

                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                using var emailClient = new SmtpClient();
                //The last parameter here is to use SSL (Which you should!)
                //emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);


                await emailClient.ConnectAsync(smtpServer, SmtpPort, SecureSocketOptions);
                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await emailClient.AuthenticateAsync(smtpUsername, smtpPassoword);

                var result = await emailClient.SendAsync(message);

                await emailClient.DisconnectAsync(true);

                List<AttachmentEmailContent>? attachments = new();

                if(emailMessage.Attachments != null)
                {
                    foreach(var attachment in emailMessage.Attachments)
                    {
                        attachments.Add(new()
                        {
                            Name = attachment.Name,
                            SubTypeMedia = attachment.SubTypeMedia,
                            TypeMedia = attachment.TypeMedia,
                            Url = attachment.Url,
                        });
                    }
                }

                var emailContent = new RegisterEmailContentRequest
                {
                    FromAddress = smtpUsername,
                    ToAddress = emailMessage.ToAddresses.Select(x => x.Address).FirstOrDefault(),
                    Body = emailMessage.Content,
                    Attachments = attachments,
                    Result = result,
                    ToPersonId = personId,
                    EmailTemplateId = emailMessage.EmailTemplateId,
                    ReferenceId = emailMessage.ReferenceId,
                    Subject = emailMessage.Subject,
                };


                await _emailContentService.RegisterEmailContentAsync(emailContent, userId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        void IEmailService.SendAsync(EmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }

        public async Task SendEmailCredentials(Person person, UserCredentials user, Guid userId)
        {
            var template = _emailTemplateRepository.GetDefault(Emails.EmailTags.Domain.Enum.EmailTagTemplateType.CREDENTIALS);
            
            if (template != null)
            {
                Dictionary<string, string> result = new()
                {
                    {"Password",user.Password},
                    {"UserName",user.UserName},
                    {"Person",person.Names +" "+ person.LastName },
                };

                string content = CommonStatic.Replace(template.Body, result);

                EmailMessage emailMessage = new()
                {
                    Content = content,
                    Subject = template.Subject,
                    ToAddresses =
                        [
                            new()
                            {
                               Address = person!.Email?.Value?? person!.PersonalEmail?.Value??"",
                               Name = person.Names
                            }
                        ],
                    ReferenceId = person.Id,
                    EmailTemplateId = template.Id,
                };

                List<AttachmentEmailDto> listAttachmentNew = await GetAttachmentById(template.Id, EntityType.EMAIL_TEMPLATE);

                if (listAttachmentNew.Count > 0)
                {
                    emailMessage.Attachments = listAttachmentNew;
                }

                await Task.FromResult(SendAsync(emailMessage, userId, person.Id));
            }
           
        }

        public async Task SendEmailCredentials(Person person, UserCredentials user, string subject, string name, Guid userId)
        {
            var email = new EmailMessage()
            {
                FromAddresses = [ new EmailAddress(){
                                Address = "OneHealth@pulsosalud.com",
                                Name = name,
                             } ],
                ToAddresses =
                [ new EmailAddress()
                                {
                                    Address = person!.Email?.Value?? person!.PersonalEmail?.Value??"",
                                    Name = person.Names
                            } ],
                Subject = subject,
                Content = EmailTemplateStatic.RecoveryPassword(person.Names, user.Password != String.Empty ? user.Password : person.DocumentNumber, user.UserName),
                ReferenceId = person.Id,
            };


            await SendAsync(email, userId, person.Id);
        }
    }
}
