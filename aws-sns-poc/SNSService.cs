using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace aws_sns_poc
{
    public interface ISNSService
    {
        Task<PublishResponse> SendSMSAsync(string phone, string subject, string Message);
    }
    public class SNSService : ISNSService
    {
        IAmazonSimpleNotificationService _SES;
        public SNSService(IAmazonSimpleNotificationService SES)
        {
            _SES = SES;
        }
        public Task<PublishResponse> SendSMSAsync(string phone, string subject, string Message)
        {
            var messageAttributes = new Dictionary<string, MessageAttributeValue>();

            MessageAttributeValue senderID = new MessageAttributeValue();
            senderID.DataType = "String";
            senderID.StringValue = "sendername";
            MessageAttributeValue sMSType = new MessageAttributeValue();
            sMSType.DataType = "String";
            sMSType.StringValue = "Transactional"; 
            MessageAttributeValue maxPrice = new MessageAttributeValue();
            maxPrice.DataType = "Number";
            maxPrice.StringValue = "0.5";
            messageAttributes.Add("AWS.SNS.SMS.SenderID", senderID);
            messageAttributes.Add("AWS.SNS.SMS.SMSType", sMSType);
            messageAttributes.Add("AWS.SNS.SMS.MaxPrice", maxPrice);
            var sendRequest = new PublishRequest()
            {
                Subject = subject,
                Message = Message,
                PhoneNumber = phone,
                MessageAttributes = messageAttributes
            };
            try
            {
                Console.WriteLine("Sending SMS using AWS SES...");
                var response = _SES.PublishAsync(sendRequest);
                Console.WriteLine("The SMS was sent successfully.");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("The SMS was not sent.");
                Console.WriteLine("Error message: " + ex.Message);
                throw new NotImplementedException();
            }
        }
}
}
