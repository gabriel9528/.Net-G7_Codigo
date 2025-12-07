namespace AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities
{
    public class Notification
    {
        private readonly List<Error> _errors = new();

        public void AddError(string message)
        {
            _errors.Add(new Error(message));
        }

        public string ErrorMessage()
        {
            return string.Join(",", _errors.Select(x => x.Message));
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }

        public List<Error> GetErrors()
        {
            return _errors;
        }

        public void CloneErrors(Notification notificationSource)
        {
            var errors = notificationSource.GetErrors();
            foreach (var error in errors)
            {
                AddError(error.Message);
            }
        }
    }
}
