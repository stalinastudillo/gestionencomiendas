CREATE DATABASE ciudades;
create user 'postal' identified by 'postal';
grant super on *.* to 'postal';
grant all privileges on *.* to 'postal' with grant option;


USE ciudades;

CREATE TABLE city (

  idcity    int(6) NOT NULL auto_increment,
  ciudad    varchar(60) default NULL,

 
   PRIMARY KEY  (idcity)
  
) ENGINE=InnoDB;

insert into city(ciudad) values ('BUENOS AIRES');
insert into city(ciudad) values ('BAHIA BLANCA');
insert into city(ciudad) values ('CORDOBA');
insert into city(ciudad) values ('ROSARIO');
insert into city(ciudad) values ('MENDOZA');
insert into city(ciudad) values ('TUCUMAN');
insert into city(ciudad) values ('LA PLATA');
insert into city(ciudad) values ('MAR DEL PLATA');
insert into city(ciudad) values ('SALTA');
insert into city(ciudad) values ('SANTA FE');
insert into city(ciudad) values ('SAN JUAN');
insert into city(ciudad) values ('RESISTENCIA');
insert into city(ciudad) values ('SANTIAGO DEL ESTERO');
insert into city(ciudad) values ('CORRIENTES');
insert into city(ciudad) values ('POSADAS');
insert into city(ciudad) values ('JUJUY');
insert into city(ciudad) values ('PARANA');
insert into city(ciudad) values ('NEUQUEN');
insert into city(ciudad) values ('FORMOSA');
insert into city(ciudad) values ('SAN LUIS');
insert into city(ciudad) values ('CATAMARCA');
insert into city(ciudad) values ('LA RIOJA');
insert into city(ciudad) values ('RIO CUARTO');
insert into city(ciudad) values ('CONCORDIA');
insert into city(ciudad) values ('COMODORO RIVADAVIA');
insert into city(ciudad) values ('SAN NICOLAS');
insert into city(ciudad) values ('SANTA ROSA');
insert into city(ciudad) values ('MERCEDES');
insert into city(ciudad) values ('SAN RAFAEL');
insert into city(ciudad) values ('TANDIL');
insert into city(ciudad) values ('BARILOCHE');
insert into city(ciudad) values ('RECONQUISTA');
insert into city(ciudad) values ('RAFAELA');
insert into city(ciudad) values ('TRELEW');
insert into city(ciudad) values ('VILLA MARIA');
insert into city(ciudad) values ('ZARATE');
insert into city(ciudad) values ('PERGAMINO');
insert into city(ciudad) values ('OLAVARRIA');
insert into city(ciudad) values ('RIO GALLEGOS');
insert into city(ciudad) values ('JUNIN');
insert into city(ciudad) values ('LUJAN');
insert into city(ciudad) values ('CAMPANA');
insert into city(ciudad) values ('SAN MARTIN');
insert into city(ciudad) values ('NECOCHEA');
insert into city(ciudad) values ('PRESIDENCIA ROQUE SAENZ PEÑA');
insert into city(ciudad) values ('GUALEGUAYCHU');
insert into city(ciudad) values ('VILLA CARLOS PAZ');
insert into city(ciudad) values ('SAN RAMON DE LA NUEVA ORAN');
insert into city(ciudad) values ('VENADO TUERTO');
insert into city(ciudad) values ('GENERAL ROCA');
insert into city(ciudad) values ('USHUAIA');
insert into city(ciudad) values ('CIPOLLETTI');
insert into city(ciudad) values ('GOYA');
insert into city(ciudad) values ('PUERTO MADRYN');
insert into city(ciudad) values ('CONCEPCION DEL URUGUAY');
insert into city(ciudad) values ('CUTRAL CO');
insert into city(ciudad) values ('RIO GRANDE');
insert into city(ciudad) values ('TARTAGAL');
insert into city(ciudad) values ('GENERAL PICO');
insert into city(ciudad) values ('SAN FRANCISCO');
insert into city(ciudad) values ('LIBERTADOR SAN MARTIN');
insert into city(ciudad) values ('SAN PEDRO');
insert into city(ciudad) values ('RIO TERCERO');
insert into city(ciudad) values ('OBERA');
insert into city(ciudad) values ('PUNTA ALTA');
insert into city(ciudad) values ('CHIVILCOY');
insert into city(ciudad) values ('AZUL');
insert into city(ciudad) values ('TRES ARROLLOS');
insert into city(ciudad) values ('MERCEDES');
insert into city(ciudad) values ('VIEDMA');
insert into city(ciudad) values ('VILLA CONSTITUCION');
insert into city(ciudad) values ('SAN LORENZO');
insert into city(ciudad) values ('ALTA GRACIA');
insert into city(ciudad) values ('NUEVE DE JULIO');
insert into city(ciudad) values ('CHACABUCO');
insert into city(ciudad) values ('GUALEGUAY');
insert into city(ciudad) values ('RAWSON');
insert into city(ciudad) values ('DOLORES');
insert into city(ciudad) values ('LICOLN');
insert into city(ciudad) values ('VEINTICINCO DE MAYO');
insert into city(ciudad) values ('VICTORIA');
insert into city(ciudad) values ('CARMEN DE PATAGONES');
insert into city(ciudad) values ('FRONTERA');
