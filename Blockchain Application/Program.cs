using Newtonsoft.Json;

namespace Blockchain_Application
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Adding new block to our blockchain
            Blockchain blockchain = new Blockchain();

            DateTime startTime = DateTime.Now;

            //blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Ruslan, receive: Eyvaz, amount: 2200 }"));
            //blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Micheal, receive: Phil, amount: 1200 }"));
            //blockchain.AddBlock(new Block(DateTime.Now, null, "{ sender: Jay, receive: Kelman, amount: 200 }"));

            blockchain.CreateTransaction(new Transaction("Ruslan", "Jay", 15));
            blockchain.ProcessPendingTransactions("Maria");
            blockchain.CreateTransaction(new Transaction("Jay", "Ruslan", 10));
            blockchain.CreateTransaction(new Transaction("Ruslan", "Ruslan", 2));
            blockchain.ProcessPendingTransactions("Maria");

            DateTime endTime = DateTime.Now;
            // Now we going to find time difference / how long has one been able to Proof of The Work (POW)
            Console.WriteLine("Time: "+ (endTime - startTime).ToString());

            Console.WriteLine("Ruslan Balance: " + blockchain.GetBalance("Ruslan").ToString());
            Console.WriteLine("Jay Balance: " + blockchain.GetBalance("Jay").ToString());
            Console.WriteLine("Maria Balance: " + blockchain.GetBalance("Maria").ToString());

            Console.WriteLine(JsonConvert.SerializeObject(blockchain,Formatting.Indented));
            // Try to check validation state and result is true.
            Console.Write(blockchain.IsValid()+ "\n");

            // Try to chech validation state after changed only one block in chain.Result is False.  
            //blockchain.Chain[1].Data = "{ sender: Micheal, receive: Phill, amount: 1200 }";
            Console.Write(blockchain.IsValid() + "\n");
            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));

            // Try to update all block in blockchain.( 51% Attack )
            blockchain.Chain[1].Hash = blockchain.Chain[1].CalculateHash();
            blockchain.Chain[2].PreviousHash = blockchain.Chain[1].Hash;
            blockchain.Chain[2].Hash = blockchain.Chain[2].CalculateHash();
            //blockchain.Chain[3].PreviousHash = blockchain.Chain[2].Hash;
            //blockchain.Chain[3].Hash = blockchain.Chain[3].CalculateHash();
            
            // Try to chech validation state after changed only one block in chain.Result is True (Bumppp).  
            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));
            Console.Write(blockchain.IsValid());
            
            Console.ReadKey();  
        }
    }
}
