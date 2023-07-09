using Newtonsoft.Json;

namespace Blockchain_Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Adding new block to our blockchain
            Blockchain blockchain = new Blockchain();
            blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Ruslan, receive: Eyvaz, amount: 2200 }"));
            blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Micheal, receive: Phil, amount: 1200 }"));
            blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Jay, receive: Kelman, amount: 200 }"));

            Console.WriteLine(JsonConvert.SerializeObject(blockchain,Formatting.Indented));
            // Try to check validation state and result is true.
            Console.Write(blockchain.IsValid()+ "\n");

            // Try to chech validation state after changed only one block in chain.Result is False.  
            blockchain.Chain[1].Data = "{ sender: Micheal, receive: Phill, amount: 1200 }";
            Console.Write(blockchain.IsValid() + "\n");
            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));

            // Try to update all block in blockchain.( 51% Attack )
            blockchain.Chain[1].Hash = blockchain.Chain[1].CalculateHash();
            blockchain.Chain[2].PreviousHash = blockchain.Chain[1].Hash;
            blockchain.Chain[2].Hash = blockchain.Chain[2].CalculateHash();
            blockchain.Chain[3].PreviousHash = blockchain.Chain[2].Hash;
            blockchain.Chain[3].Hash = blockchain.Chain[3].CalculateHash();
            
            // Try to chech validation state after changed only one block in chain.Result is True (Bumppp).  
            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));
            Console.Write(blockchain.IsValid());
            
            Console.ReadKey();  
        }
    }
}
