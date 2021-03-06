﻿using DBconnectShop.Addons;
using DBconnectShop.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DBconnectShop.Access {
    /// <summary>
    /// Informacje o użytkowniku
    /// </summary>
    public class UserProfil {
        private User user;
        int id;
        /// <summary>
        /// Login użytkownika
        /// </summary>
        public string UserName =>
            (user.User_name is null) ?
                "" : user.User_name.Trim();
        /// <summary>
        /// Imie
        /// </summary>
        public string FirstName =>
            (user.User_Data is null) ?
                "" : user.User_Data.User_first_name.Trim();
        /// <summary>
        /// Drugie imie
        /// </summary>
        public string SecoundName =>
            (user.User_Data is null) ?
                "" : user.User_Data.User_second_name.Trim();
        /// <summary>
        /// Nazwisko
        /// </summary>
        public string FamilyName =>
            (user.User_Data is null) ?
                "" : user.User_Data.User_family_name.Trim();
        /// <summary>
        /// Lista adresów
        /// </summary>
        public IReadOnlyList<User_address> Addresses =>
            user.User_Address.ToList().AsReadOnly();
        /// <summary>
        /// Lista adresów
        /// </summary>
        public IReadOnlyList<Address> Address =>
            user.User_Address.Select(a => a.Address).ToList().AsReadOnly();
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="login">Autoryzacja</param>
        public UserProfil(Login login) {
            int userID = 0;

            try {
                userID = login.GetUserID;
            } catch {
                throw new AuthorizationException();
            }

            id = userID;
            Reload();
        }
        /// <summary>
        /// Odswieża dane użytkownika
        /// </summary>
        public void Reload() {
            using var db = new Shop();

            var users = db.Users
                .Include(a => a.User_Data)
                .Include(a => a.User_Address)
                    .ThenInclude(b => b.Address)
                .Where(a => a.User_id == id);

            this.user = users.FirstOrDefault();
        }
        /// <summary>
        /// Zmiana danych
        /// </summary>
        /// <param name="FirstName">Nowe imie</param>
        /// <param name="SecoundName">Nowe drugie imie</param>
        /// <param name="FamilyName">Nowe nazwisko</param>
        public void Change_Data(string FirstName, string SecoundName, string FamilyName) {
            using var db = new Shop();
            int userID = 0;

            try {
                userID = user.User_id;
            } catch {
                throw new AuthorizationException();
            }

            if(FirstName.Length < 3 || FamilyName.Length < 3)
                throw new AddElementException("Nie wszystkie wymagane pola zostały poprawnie wypełnione.\nSprawdz i spróbuj ponownie.");

            if(user.User_Data is null) {
                var data = new User_data() {
                    User_id = userID,
                    User_first_name = FirstName,
                    User_second_name = SecoundName,
                    User_family_name = FamilyName
                };

                db.Users_Data.Add(data);
            } else {
                var data = db.Users_Data.First(a => a.User_id == userID);

                data.User_first_name = FirstName;
                data.User_second_name = SecoundName;
                data.User_family_name = FamilyName;

                db.Users_Data.Attach(data);
            }

            int code = db.SaveChanges();
            if(code != 1)
                throw new AddElementException("Wystąpił problem z przesłanymi danymi.");
        }

        /// <summary>
        /// Zmiana avatara
        /// </summary>
        /// <param name="file">Sciezka do zdjęcia</param>
        public void Change_Avatar(string file) {
            using var db = new Shop();

            var image = new Image(file);
            var User_Data = user.User_Data;
            if(User_Data is null)
                throw new AddElementException("Aby dodać miniaturkę musisz posiadać wypełniony profil użytkownika.");

            db.Users_Data.Attach(User_Data);

            User_Data.User_avatar = image.BlobImage;

            try {
                int code = db.SaveChanges();
                if(code != 1)
                    throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            } catch {
                throw new AddElementException("Wystąpił problem z przesłanym avatarem.");
            }
        }

        /// <summary>
        /// Dodaje nowy address użytkownikowi
        /// </summary>
        /// <param name="country">Kraj</param>
        /// <param name="city">Miasto</param>
        /// <param name="street">Ulica</param>
        /// <param name="building">Numer budynku</param>
        /// <param name="zipCode">Kod pocztowy</param>
        /// <returns>Zwraca ID dodanego adresu</returns>
        public int AddAddress(string country, string city, string street, string building, string zipCode) {
            using var db = new Shop();

            if(country.Length < 3 || city.Length < 3 || street.Length < 3)
                throw new ArgumentException("Uzupełnij wszystkie dane.");

            country = country.First().ToString().ToUpper() + country.Substring(1).ToLower();
            city = city.First().ToString().ToUpper() + city.Substring(1).ToLower();
            street = street.First().ToString().ToUpper() + street.Substring(1).ToLower();

            if(!Regex.IsMatch(country, @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$"))
                throw new ArgumentException("Nie poprawna nazwa kraju.");
            if(!Regex.IsMatch(city, @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$"))
                throw new ArgumentException("Nie poprawna nazwa miasta.");
            if(!Regex.IsMatch(street, @"^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$"))
                throw new ArgumentException("Nie poprawna nazwa ulicy.");
            if(!int.TryParse(building, out _))
                throw new ArgumentException("Nie poprawny numer mieszkania.");
            if(!Regex.IsMatch(zipCode, @"^[0-9]{2}-[0-9]{3}$"))
                throw new ArgumentException("Nie poprawny kod pocztowy.");

            Address address = db.Addresses.Where(a =>
                a.Address_country == country &&
                a.Address_city == city &&
                a.Address_street == street &&
                a.Address_building_number == building &&
                a.Address_zip_code == zipCode).FirstOrDefault();

            if(address is null) {
                address = new Address() {
                    Address_country = country,
                    Address_city = city,
                    Address_street = street,
                    Address_building_number = building,
                    Address_zip_code = zipCode
                };
            }

            var userAddress = new User_address() {
                User_id = user.User_id,
                Address = address
            };
            db.User_Addresses.Add(userAddress);

            try {
                int code = db.SaveChanges();
                if(code == 0)
                    throw new AddElementException("Wystąpił problem z przesłanym adressem.");
                return userAddress.User_Address_id;
            } catch {
                throw new AddElementException("Wystąpił problem z przesłanym adressem.");
            }
        }
    }
}
