using ClassesMetier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        MySqlConnection cnx;
        MySqlCommand cmd;
        MySqlDataReader dr;

        public GstBdd()
        {
            string driver = "server=localhost;user id=root;password=;database=gestionsalles";
            cnx = new MySqlConnection(driver);
            cnx.Open();
        }

        public List<Manifestation> GetAllManifestations()
        {
            return null;
        }

        public List<Place> GetAllPlacesByIdManifestation(int idManif,int idSalle)
        {
            return null;
        }

        public List<Tarif> GetAllTarifs()
        {
            return null;
        }

        public void ReserverPlace(int idPlace, int idSalle,int idManif)
        {
            
        }
    }
}
