using Newtonsoft.Json;
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
        public IList<Transaction> Transactions { get; set; }
        //public string Data { get; set; }
        public int Nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            this.Index = 0;
            this.TimeStamp = timeStamp;
            this.PreviousHash = previousHash;
            this.Transactions = transactions;
            //this.Data = data;
            //this.Hash = CalculateHash(); 
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outBytes = sha256.ComputeHash( inBytes );
            return Convert.ToBase64String(outBytes);
        }

        public void Mine(int difficulty)
        {
            string leadingZeros = new string('0', difficulty);
            while(this.Hash == null || this.Hash.Substring(0,difficulty) != leadingZeros) 
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}