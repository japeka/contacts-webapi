namespace ContactsWebApi.Models
{

    /*User-luokka, jolla propertyna ainakin etunimi ja sukunimi.
     * Muodostetaan Azuresta saatavilla tiedoilla.*/
    public class User
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public User() { }

    }
}
