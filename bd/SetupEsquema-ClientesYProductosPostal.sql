create database gestionNuevo;
create user 'postal' identified by 'postal';
grant super on *.* to 'postal';
grant all privileges on *.* to 'postal' with grant option;

USE gestionNuevo;

CREATE TABLE `clientes` (
  `idcliente` int(30) unsigned NOT NULL AUTO_INCREMENT,
  `cuil` varchar(13) DEFAULT NULL,
  `nombre` varchar(30) NOT NULL,
  `apellido` varchar(45) DEFAULT NULL,
  `direccion` varchar(45) DEFAULT NULL,
  `ciudad` varchar(45) DEFAULT NULL,
  `codigo_postal` varchar(10) DEFAULT NULL,
  `telefono` varchar(25) DEFAULT NULL,
  `celular` varchar(30) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `porcentaje_vd` float(8,2) unsigned DEFAULT NULL,
  `porcentaje_cr` float(8,2) unsigned DEFAULT NULL,
  `credito` float(8,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`idcliente`)
) ENGINE=InnoDB;

INSERT INTO `clientes` VALUES (1,NULL,'Usuario General','',NULL,NULL,NULL,NULL,NULL,NULL,1.00,1.00,0.00),(2,'20-05496794-3','Amodeo Guillermo','','Chiclana 451','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(3,'27-10977246-7','Fernandino Emilce','','Alsina 229','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(4,'20-12278672-3','Bio Arg','','Panama 828','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(5,'30-70838934-6','Cetac','','Alsina 19','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(6,'30-70766005-4','Ciname Center','','B.B.Plaza Shopping','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(7,'30-52616080-7','Club Olimpo','','Sarmiento','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(8,'30-60866381-5','Control Union','','','ING. WHITE','8103','','','',1.00,1.00,0.00),(9,'30-54184930-7','Dietrich','','Sarmiento 3795','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(10,'33-69588914-9','Editorial Ediba SRL','','Brown 474','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(11,'  -        -','Emilce','','Alsina 229','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(13,'30-70747673-3','Parque Trelew','','Av. La Plata 2640','TRELEW','','','','',1.00,1.00,0.00),(14,'  -        -','Farmacia Diba BHI','','Sarmiento 34','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(15,'  -        -','Farmacia Diba','','','PUNTA ALTA','','','','',1.00,1.00,0.00),(16,'30-60605463-3','Frigorifico Villa Olga','','Ruta 3 KM 699','GENERAL CERRI','','','','',1.00,1.00,0.00),(17,'  -        -','Raul Gaitan','','Jackson Ville 86','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(18,'  -        -','Gladis Galvan','','','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(19,'20-23867971-1','Graff Ediciones','','','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(20,'30-68732879-1','Grainco SA','','Sarmiento 4100','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(21,'33-68244727-9','Inspectorate de Arg.','','Sarmiento 1113 P4','CAPITAL FEDERAL','','','','',1.00,1.00,0.00),(22,'  -        -','Jaque','','Castelli 124','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(23,'  -        -','Laboratorio Gama','','Av. Alem 492','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(24,'30-51824288-8','Hector Losi','','Don Bosco 4075','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(25,'  -        -','Optica Louro','','Alsina 91','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(26,'  -        -','Lucas','','','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(27,'  -        -','Maturi','','Vicente Lopez','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(28,'  -        -','Maxi Castellano','','Belgrano 118','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(29,'30-62659215-1','Merlino','','Zapiola 751','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(30,'30-71104814-2','Suncell S.A.','','Estomba 137','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(31,'20-17433493-6','Optica Fortunato','','O´higgins 232','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(32,'30-69900033-3','Gustavo Rigoni','','','CAPITAL FEDERAL','','','','',1.00,1.00,0.00),(33,'  -        -','Autopartes Palermo','','Beruti','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(34,'27-02765622-1','Pieles Chic','','Drago 40','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(35,'30-69627684-2','Prome','','','DEL VIZO','','','','',1.00,1.00,0.00),(36,'30-70880884-5','Bahia Verde','','Castelar 1740','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(39,'  -        -','Salvarezza','','Av. Alem','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(40,'30-70803281-2','Sermex S.A.','','9 de Julio 461','BAHIA BLANCA','8000','','','',1.50,1.50,0.00),(41,'  -        -','Secom','','Mendoza 699','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(42,'27-24095470-8','Larrere Maria Ana','','Brown 334','BAHIA BLANCA','8000','','','',1.00,1.00,0.00),(43,'30-58551260-1','Comahue S.R.L.','','25 de Mayo 120','BAHIA BLANCA','8000','0291-4551914/5','','',1.00,1.00,0.00),(44,'30-70828643-1','Urbano Express S.A.','','San Jose 1788','CAPITAL FEDERAL','','','','',1.00,1.00,0.00),(46,'30-70926584-5','Editorial PGP','','Talcahuano 231','CAPITAL FEDERAL','','','','',1.00,1.00,0.00),(47,'  -        -','Multiequip','','','BAHIA BLANCA','8000','','','',1.00,1.00,0.00);

CREATE TABLE `empresa` (
  `idempresa` int(30) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) DEFAULT NULL,
  `direccion` varchar(45) DEFAULT NULL,
  `telefono` varchar(45) DEFAULT NULL,
  `ciudad` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idempresa`)
) ENGINE=InnoDB;


CREATE TABLE `factura` (
  `idfactura` int(30) unsigned NOT NULL AUTO_INCREMENT,
  `fechaCreacion` date DEFAULT NULL,
  `fechaFirst` date DEFAULT NULL,
  `fechaLast` date DEFAULT NULL,
  `total` float(8,2) unsigned DEFAULT NULL,
  `flete` float(8,2) unsigned DEFAULT NULL,
  `seguro` float(8,2) unsigned DEFAULT NULL,
  `descripcion` text,
  `ivari` float(8,2) unsigned DEFAULT NULL,
  `neto` float(8,2) unsigned DEFAULT NULL,
  `credito` float(8,2) unsigned DEFAULT NULL,
  `cond_vta` varchar(45) DEFAULT NULL,
  `idcliente` int(30) unsigned DEFAULT NULL,
  `tipo` varchar(20) DEFAULT NULL,
  `iduser` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idfactura`)
) ENGINE=InnoDB;


CREATE TABLE `pago_cheque` (
  `idcheque` int(45) unsigned NOT NULL AUTO_INCREMENT,
  `nro_cheque` int(45) DEFAULT NULL,
  `banco` varchar(45) DEFAULT NULL,
  `monto` float(8,2) unsigned DEFAULT NULL,
  `nro_recibo` int(45) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `idfactura` int(45) NOT NULL,
  PRIMARY KEY (`idcheque`)
) ENGINE=InnoDB;


CREATE TABLE `pago_contado` (
  `idcontado` int(45) unsigned NOT NULL AUTO_INCREMENT,
  `fecha` date DEFAULT NULL,
  `nro_recibo` int(45) DEFAULT NULL,
  `monto` float(8,2) unsigned DEFAULT NULL,
  `idfactura` int(45) NOT NULL,
  PRIMARY KEY (`idcontado`)
) ENGINE=InnoDB;


CREATE TABLE `pago_factura` (
  `idfactura` int(45) NOT NULL,
  `pagado` tinyint(1) DEFAULT '0',
  `ibb` float(8,2) unsigned DEFAULT NULL,
  `ganancias` float(8,2) unsigned DEFAULT NULL,
  `suss` float(8,2) unsigned DEFAULT NULL,
  `saldo` float(8,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`idfactura`)
) ENGINE=InnoDB;


CREATE TABLE `producto` (
  `codigo` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `precio` float(8,2) unsigned NOT NULL,
  `idcliente` int(10) unsigned NOT NULL,
  `descripcion` text,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB;


INSERT INTO `producto` VALUES (1,'Caja',27.00,3,''),(2,'Sobre',20.00,2,''),(3,'Caja',20.00,2,''),(4,'Bolsa',24.00,2,''),(5,'Sobre',26.00,4,''),(6,'Caja',26.00,4,''),(7,'Sobre',24.00,5,''),(8,'Caja',24.00,5,''),(12,'Sobre',24.00,7,''),(13,'Caja',26.00,7,''),(14,'Sobre',20.00,8,''),(15,'Caja',24.00,8,''),(16,'Sobre',27.00,9,''),(17,'Caja',27.00,9,''),(18,'Bolsin',27.00,9,''),(19,'Sobre 24hs',23.00,10,''),(20,'Caja 24hs',27.00,10,''),(21,'Gestion h/1000',37.00,10,''),(22,'Sobre/Caja Prov',36.00,10,''),(23,'Gestion Prov',45.00,10,''),(25,'Caja',25.00,11,''),(26,'Sobre',23.96,13,''),(27,'Caja',23.96,13,''),(28,'Sobre',24.00,14,''),(29,'Caja',24.00,14,''),(30,'Sobre',24.00,15,''),(31,'Caja',26.00,15,''),(32,'Sobre 24hs',30.00,16,''),(33,'Caja 24hs',30.00,16,''),(34,'Sobre',26.00,17,''),(35,'Caja',26.00,17,''),(36,'Sobre',25.00,18,''),(37,'Caja',27.00,18,''),(38,'Gestion',45.00,18,''),(39,'Sobre',26.00,19,''),(40,'Caja',26.00,19,''),(41,'Bolsin',26.00,20,''),(42,'Sobre',19.83,20,''),(45,'Sobre',26.00,22,''),(46,'Caja',26.00,22,''),(47,'Gestion',45.00,22,''),(48,'Sobre',24.00,23,''),(49,'Caja',26.00,23,''),(50,'Cap Fed',27.00,24,''),(51,'Gran Bs As',33.00,24,''),(52,'Sobre',26.00,25,' '),(53,'Caja',26.00,25,''),(55,'Sobre',26.00,27,''),(56,'Caja',26.00,27,''),(57,'Sobre',25.00,28,''),(58,'Caja',28.00,28,''),(59,'Sobre',24.00,29,''),(60,'Caja',26.00,29,''),(61,'Caja Chica',24.79,30,''),(62,'Caja Grande',26.44,30,''),(63,'Sobre',24.00,31,''),(64,'Caja',26.00,31,''),(65,'Caja',23.14,32,''),(66,'Sobre',21.00,33,''),(67,'Caja',24.00,33,''),(68,'Caja',28.00,34,''),(69,'Sobre',27.00,35,''),(70,'Caja',35.00,35,''),(71,'Sobre',26.00,36,''),(72,'Caja',26.00,36,''),(73,'Caja',28.00,39,''),(74,'Sobre',24.00,40,''),(75,'Caja',26.00,40,''),(76,'Servicio Esp',32.00,40,''),(77,'Sobre P.Alta',11.00,14,''),(78,'Sobre',30.00,41,''),(79,'Caja',35.00,41,''),(81,'Bolsin',23.96,13,''),(82,'Sobre BHI',11.00,15,''),(83,'Sobre',26.00,6,''),(84,'Caja',26.00,6,''),(85,'Bolsa',28.00,6,''),(86,'Gestion',45.00,24,''),(87,'Caja P.Alta',11.00,14,''),(88,'Caja BHI',11.00,15,''),(89,'Caja',15.00,42,''),(90,'Caja',35.00,1,''),(91,'Servicio Especial',32.00,7,''),(92,'Caja',24.79,43,''),(93,'Sobre',20.66,43,''),(94,'Caja',28.92,43,''),(96,'Colectora BUE/BHI',14.88,44,''),(97,'Colectora BHI/BUE',18.19,44,''),(98,'Caja BUE/BHI',9.92,44,''),(99,'Caja BHI/BUE',13.23,44,''),(102,'Revista',5.00,46,''),(103,'Kg',1.00,46,''),(104,'Caja',28.92,47,''),(105,'Sobre',24.79,47,''),(106,'Gestion',37.19,47,''),(107,'Getion',45.00,20,''),(108,'Caja Extra',15.00,10,''),(109,'Sobre Extra',15.00,10,''),(110,'Caja Urgente',70.00,16,''),(111,'Sobre Urgente',70.00,16,''),(112,'Caja Grande Pesada',30.00,3,''),(113,'Caja',26.00,20,''),(114,'Cadena de Frio',32.00,23,''),(115,'Cadena de Frio',37.00,16,''),(116,'Caja Grande',27.00,33,''),(117,'Sobre Fin de Año',24.00,40,'');

CREATE TABLE `productos_vender` (
  `codprod` int(30) unsigned NOT NULL,
  `cant` int(30) DEFAULT NULL,
  `descr` text,
  `kgs` float(8,2) unsigned DEFAULT NULL,
  `prec` float(8,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`codprod`)
) ENGINE=InnoDB;


CREATE TABLE `productos_vendidos` (
  `idprod_vendido` int(30) unsigned NOT NULL AUTO_INCREMENT,
  `nroprod_vendido` int(30) unsigned DEFAULT NULL,
  `idremito` int(30) NOT NULL,
  `cantidad` int(30) DEFAULT NULL,
  `descripcion` text,
  `kg` float(8,2) unsigned DEFAULT NULL,
  `precio` float(8,2) unsigned DEFAULT NULL,
  PRIMARY KEY (`idprod_vendido`)
) ENGINE=InnoDB;


CREATE TABLE `remito` (
  `idremito` int(30) unsigned NOT NULL AUTO_INCREMENT,
  `fecha` date DEFAULT NULL,
  `remitente` varchar(30) DEFAULT NULL,
  `domicilior` varchar(45) DEFAULT NULL,
  `localidadr` varchar(30) DEFAULT NULL,
  `cuilr` varchar(15) DEFAULT NULL,
  `destinatario` varchar(30) DEFAULT NULL,
  `domiciliod` varchar(45) DEFAULT NULL,
  `localidadd` varchar(30) DEFAULT NULL,
  `cuild` varchar(15) DEFAULT NULL,
  `cond_vta` varchar(20) DEFAULT NULL,
  `iva` varchar(30) DEFAULT NULL,
  `valor_declarado` float(8,2) unsigned DEFAULT NULL,
  `contrareembolso` float(8,2) unsigned DEFAULT NULL,
  `flete` float(8,2) unsigned DEFAULT NULL,
  `seguro` float(8,2) unsigned DEFAULT NULL,
  `liquidado` varchar(1) DEFAULT NULL,
  `observacion` varchar(45) DEFAULT NULL,
  `idcliente` int(10) unsigned DEFAULT NULL,
  `nro_remito` varchar(10) DEFAULT NULL,
  `iduser` int(6) NOT NULL,
  `total` float(8,2) unsigned DEFAULT NULL,
  `idfactura` int(30) unsigned DEFAULT NULL,
  PRIMARY KEY (`idremito`)
) ENGINE=InnoDB;


CREATE TABLE `remitosaliquidar` (
  `idremito` int(30) NOT NULL
) ENGINE=InnoDB;



DELIMITER ;;
CREATE PROCEDURE `aumento_producto`(in codigo_producto int(10) unsigned, in porcentaje float(8,4))
begin
        declare cambio float(8,4) unsigned;
        
        if porcentaje > 0 then
            set cambio = (porcentaje / 100) + 1;
        else
            set cambio = 1 - ((porcentaje * (-1)) / 100);
        end if;
        
        update producto set precio = precio * cambio where (codigo = codigo_producto);
    end ;;
	
DELIMITER ;

DELIMITER ;;
CREATE PROCEDURE `crear_factura`(in fechaCreacion date,in fechaFirst date,in fechaLast date, in descripcion Text,in tipo varchar(20),in iduser varchar(45), in idfact int(30) unsigned)
begin

    declare MasRemitos boolean default true;
    declare valorTotal float(8,2) default 0;
    declare fleteTotal float(8,2) default 0;
    declare seguroTotal float(8,2) default 0;
	declare valorNeto float(8,2) default 0;
	declare valorIvaRi float(8,2) default 0;
    declare fleteActual float(8,2);
    declare seguroActual float(8,2);
	declare creditoCliente float(8,2);
	declare newCredit float(8,2) default 0;
    declare id int(30) unsigned;
	declare idc int(30) unsigned;
	declare condVenta varchar(45);
	

        declare cursor_remitos cursor for select * from remitosaLiquidar;
		declare cursor_condVta cursor for select cond_vta from remito where idremito=id;
        declare cursor_seguro cursor for select (seguro) from remito where (idremito=id);
		declare cursor_flete cursor for select (flete) from remito where (idremito=id);
		declare cursor_cliente cursor for select idcliente from remito where(idremito=id);
		declare cursor_creditoCliente cursor for select credito from clientes where(idcliente=idc);
        declare continue handler for not found set MasRemitos = false;

        start transaction;
        
            insert into factura(idfactura,fechaCreacion,fechaFirst,FechaLast,descripcion,credito,tipo,iduser) values (idfact,fechaCreacion,fechaFirst,FechaLast,descripcion,0,tipo,iduser);
	
            open cursor_remitos;
            fetch cursor_remitos into id;
			
			open cursor_condVta;
            fetch cursor_condVta into condVenta;
            close cursor_condVta;
			
			open cursor_cliente;
			fetch cursor_cliente into idc;
			close cursor_cliente;
			
			open cursor_creditoCliente;
			fetch cursor_creditoCliente into creditoCliente;
			close cursor_creditoCliente;
			
			
            repeat
                open cursor_flete;
                fetch cursor_flete into fleteActual;
                close cursor_flete;
				
				open cursor_seguro;
                fetch cursor_seguro into seguroActual;
                close cursor_seguro;
                
                set fleteTotal = fleteTotal + fleteActual;
                set seguroTotal= seguroTotal + seguroActual;
                
                update remito set liquidado='1' where idremito=id;
                update remito set idfactura=idfact where idremito=id;
                
                fetch next from cursor_remitos into id;
            until (MasRemitos = false) 
            end repeat;
            close cursor_remitos;
            
            set valorTotal=(fleteTotal+seguroTotal);
			set valorNeto=(fleteTotal+seguroTotal);
			set valorIvaRi=(valorTotal*21)/100;
            set valorTotal=valorTotal*1.21;

			if(valorTotal>=creditoCliente) then
				set newCredit=0;
			else
				set newCredit=-(valorTotal-creditoCliente);
			end if;
			
			update factura set total=valorTotal where idfactura=idfact;
           update factura set flete=fleteTotal where idfactura=idfact;
			update factura set seguro=seguroTotal where idfactura=idfact;
			update factura set neto=valorNeto where idfactura=idfact;
			update factura set ivari=valorIvaRi where idfactura=idfact;
			update factura set credito=creditoCliente where idfactura=idfact;
			update factura set cond_vta=condVenta where idfactura=idfact;
			update factura set idcliente=idc where idfactura=idfact;
			update clientes set credito=newCredit where idcliente=idc;

		 insert into pago_factura(idfactura,pagado,ibb,ganancias,suss,saldo) values(idfact,false,null,null,null,valorTotal);
        commit;
    end ;;
DELIMITER ;

DELIMITER ;;
CREATE PROCEDURE `crear_remito`(in fecha date, in remitente varchar(30), in domicilior varchar(45), in localidadr varchar(30), in cuilr varchar(15), in destinatario varchar(30), in domiciliod varchar(45), in localidadd varchar(30), in cuild varchar(15), in cond_vta varchar(20), in iva varchar(30), in valor_declarado float(8,2), in contrareembolso float(8,2), in flete float(8,2), in seguro float(8,2),in observacion varchar(45), in idcliente int(10), in nro_remito varchar(10), in iduser int(6))
begin
        
        declare valorTotal float(8,2);
        declare nroremito int(30);
        declare codprod int(30) unsigned;
        declare cant int(30);
        declare descr text;
        declare kgs float(8,2) unsigned;
	declare kgs2 float(8,2) unsigned;
        declare prec float(8,2) unsigned;
        declare MasProductos boolean default true;
		
		

        
        
		declare cursor_remito cursor for select idremito from remito where (idremito = (select max(idremito) from remito));
        
        declare cursor_cargar cursor for select * from productos_vender;
        declare continue handler for not found set MasProductos = false;
        
        start transaction;    
        
            set valorTotal= flete+seguro;
            
            insert into remito(fecha,remitente,domicilior,localidadr,cuilr,destinatario,domiciliod,localidadd,cuild,cond_vta,iva,valor_declarado,contrareembolso,flete,seguro,liquidado,observacion,
            idcliente,nro_remito,iduser,total) values (fecha,remitente,domicilior,localidadr,cuilr,destinatario,domiciliod,localidadd,cuild,cond_vta,iva,valor_declarado,contrareembolso,flete,seguro,'0',observacion,
            idcliente,nro_remito,iduser,valorTotal);
            
            open cursor_remito;
            fetch cursor_remito into nroremito;
            close cursor_remito;
            
            open cursor_cargar;
            fetch  next from cursor_cargar into codprod, cant, descr, kgs, prec;
            repeat
             
			 
	
			 
			 
               insert into productos_vendidos(nroprod_vendido,idremito,cantidad,descripcion, kg ,precio) values (codprod,nroremito, cant, descr, kgs, prec);
			   
			
				
                
                fetch next from cursor_cargar into codprod, cant, descr, kgs, prec;
            until (MasProductos = false) 
            end repeat;
            
            close cursor_cargar;
            
        commit;
    end ;;
DELIMITER ;

	insert into empresa (nombre,direccion,telefono,ciudad) values ('Nueva Empresa','Brown 814','(0291)4531704','Bahia Blanca');