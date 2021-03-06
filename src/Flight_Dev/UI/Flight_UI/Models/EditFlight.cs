using System.Collections.Generic;

namespace Flight_UI.Models
{
    public class EditFlight
    {
       
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string DepartureDate { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public List<ClientModel> ListClient { get; set; }

        public EditFlight()
        {
            ListClient = new List<ClientModel>();
        }

    }
}
