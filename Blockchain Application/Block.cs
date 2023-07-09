using System.Security.Cryptography;
using System.Text;

namespace Blockchain_Application
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }

        public Block(DateTime timeStamp, string previousHash, string data)
        {
            this.Index = 0;
            this.TimeStamp = timeStamp;
            this.PreviousHash = previousHash;
            this.Data = data;
            this.Hash = CalculateHash(); 
        }
        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");
            byte[] outBytes = sha256.ComputeHash( inBytes );
            return Convert.ToBase64String(outBytes);
        }
    }
}