# _Hair Salon Client/Employer Database_

#### _C# MSTest exercise, September 2018_

#### By _**Mel Yasuda**_

## Description
It is an app for a hair salon. The owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist. The stylists work independently, so each client only belongs to a single stylist.

## Specification
* The program shows a list of all the stylists.
* The program allows the user to select a stylist, see their details, and see a list of all clients that belong to that stylist.
* The program allows the user to add new stylists to the system when they are hired.
* The program allows the user to add new clients to a specific stylist.
* The program won't allow the user to add a client if no stylists have been added.
* The program allows the user to delete stylists (all and single).
* The program allows the user to delete clients (all and single).
* The program allows the user to view clients (all and single).
* The program allows the user to edit JUST the name of a stylist. (You can choose to allow employees to edit additional properties but it is not required.)
* The program allows the user to edit ALL of the information for a client.
* The program allows the user to add a specialty and view all specialties that have been added.
* The program allows the user to add a specialty to a stylist.
* The program allows the user to click on a specialty and see all of the stylists that have that specialty.
* The program allows the user to see the stylist's specialties on the stylist's details page.
* The program allows the user to add a stylist to a specialty.

## Setup/Installation Requirements
1. Clone the following repository: https://github.com/MelYasuda/HairSalon.Solution
2. On terminal, go to HairSalon directory
3. Restore Dotnet
4. Build Dotnet
5. Open http://localhost:5000 on a browser

* Database Creation Instruction (Use MySQL)
1. CREATE DATABASE mel_yasuda;
2. USE mel_yasuda
3. CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
4. CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT(11));
5. CREATE TABLE specialities (id serial PRIMARY KEY, description VARCHAR(255));
6. CREATE TABLE stylists_specialities (id serial PRIMARY KEY, stylist_id INT(11), speciality_id INT(11));

## Known Bugs
TBD

## Support and contact details
* Mel, yasudamel@gmail.com

## Technologies Used
* html
* CSS
* Javascript
* C#
* MS Test

### License

Copyright (c) 2018 **_Mel Yasuda, Epicodus_**
