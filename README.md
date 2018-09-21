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

## Setup/Installation Requirements
1. Clone the following repository: https://github.com/MelYasuda/HairSalon.Solution
2. On terminal, go to HairSalon directory
3. Restore Dotnet
4. Build Dotnet

* Database Creation Instruction (Use MySQL)
1. CREATE DATABASE mel_yasuda;
2. USE mel_yasuda
3. CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
4. CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT(11));

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
