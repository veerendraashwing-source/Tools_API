namespace Easychit_Repository.Interfaces.Easy_Chit_Tools
{
    public class NameChangeDto
    {
        public string contact_id;

        public string NewName { get; set; }
        public string NewArea { get; set; }
        public string NewCity { get; set; }
        public string NewPincode { get; set; }
        public string NewAddress { get; set; }
        public string NewMobileNo { get; set; }
        public string NewSurname { get; set; }
        public string NewMailingName { get; set; }
    }
}