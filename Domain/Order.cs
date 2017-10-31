using System;

namespace Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int BatchNumber { get; private set; }
        public string SourceSystemName { get; private set; }
        public DateTime EnteredOn { get; private set; }
        public string Description { get; set; }


        public Order()
        {
            
        }

        public Order(int batchNumber, string sourceSystemName,DateTime enteredOn,string description)
        {
            BatchNumber = batchNumber;
            SourceSystemName = sourceSystemName;
            EnteredOn = enteredOn;
            Description = description;
        }

    }
}
