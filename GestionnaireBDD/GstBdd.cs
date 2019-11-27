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
            string driver = "server=localhost;user id=root;password=;database=reservation";
            cnx = new MySqlConnection(driver);
            cnx.Open();
        }

        public List<Manifestation> GetAllManifestations()
        {
            List<Manifestation> lesManifs = new List<Manifestation>();
            cmd = new MySqlCommand("SELECT idManif, nomManif, dateDebut, dateFin, idSalle, nomSalle, nbPlaces FROM manifestation INNER JOIN salle ON numSalle = idSalle", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Salle laSalle = new Salle()
                {
                    IdSalle = Convert.ToInt16(dr[4].ToString()),
                    NomSalle = dr[5].ToString(),
                    NbPlaces = Convert.ToInt16(dr[6].ToString()),
                };

                Manifestation uneManif = new Manifestation()
                {
                    IdManif = Convert.ToInt16(dr[0].ToString()),
                    NomManif = dr[1].ToString(),
                    DateDebutManif = dr[2].ToString(),
                    DateFinManif = dr[3].ToString(),
                    LaSalle = laSalle
                };
                lesManifs.Add(uneManif);              
            }
            dr.Close();
            return lesManifs;
        }

        public List<Place> GetAllPlacesByIdManifestation(int idManif,int idSalle)
        {
            List<Place> lesPlaces = new List<Place>();
            cmd = new MySqlCommand("SELECT idPlace, numTarif, libre, prix FROM manifestation INNER JOIN occuper ON manifestation.idManif = occuper.numManif INNER JOIN place ON occuper.numPlace = place.idPlace INNER JOIN tarif ON place.numTarif = tarif.idTarif WHERE manifestation.idManif = " +idManif+ " and place.numSalle = "+idSalle, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                char etat = 'o';
                if (Convert.ToBoolean(dr[2].ToString()) == false)
                {
                    etat = 'l';
                }
                Place unePlace = new Place()
                {
                    IdPlace = Convert.ToInt16(dr[0].ToString()),
                    CodeTarif = Convert.ToChar(dr[1].ToString()),
                    Occupee = Convert.ToBoolean(dr[2].ToString()),
                    Prix = Convert.ToDouble(dr[3].ToString()),
                    Etat = etat
                };
                lesPlaces.Add(unePlace);
            }
            dr.Close();
            return lesPlaces;
        }

        public List<Tarif> GetAllTarifs()
        {
            List<Tarif> lesTarifs = new List<Tarif>();
            cmd = new MySqlCommand("SELECT * FROM tarif", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Tarif unTarif = new Tarif()
                {
                    IdTarif = Convert.ToChar(dr[0].ToString()),
                    NomTarif = dr[1].ToString(),
                    Prix = Convert.ToDouble(dr[2].ToString())
                };
                lesTarifs.Add(unTarif);
            }
            dr.Close();
            return lesTarifs;
        }

        public void ReserverPlace(int idPlace, int idSalle,int idManif)
        {
            cmd = new MySqlCommand("UPDATE occuper SET libre = 1 WHERE numManif= "+idManif+"AND numPlace= "+idPlace+"AND numSalle= "+idSalle+"", cnx);
            cmd.ExecuteNonQuery();
        }
    }
}
