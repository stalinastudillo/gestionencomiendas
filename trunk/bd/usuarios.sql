CREATE DATABASE users;


create user 'usuarios'identified by 'usuarios';


grant super on *.* to 'usuarios';
grant all privileges on *.* to 'usuarios' with grant option;



USE users;

CREATE TABLE `usuario`(

  `iduser`    int(6) NOT NULL auto_increment,
  `nombre`    varchar(30) NOT NULL,
  `password`      varchar(30)  NOT NULL,

 
   PRIMARY KEY  (`iduser`)
  
) ENGINE=InnoDB;

insert into usuario(nombre,password) values ('admin','admin');