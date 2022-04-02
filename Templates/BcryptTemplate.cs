namespace MinimalTwitterApi.Templates
{
    public abstract class BcryptTemplate
    {
        protected abstract string GetSalt();

        public string DoHash(string data)
        {
            return BCrypt.Net.BCrypt.HashPassword(data + GetSalt());
        }

        public bool IsCompareValid(string data, string hashedData)
        {
            return BCrypt.Net.BCrypt.Verify(data + GetSalt(), hashedData);
        }
    }
}