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

	insert into clientes (idcliente,nombre,apellido,porcentaje_vd,porcentaje_cr,credito ) values (1,'Usuario General','',1,1,0);
	insert into empresa (nombre,direccion,telefono,ciudad) values ('Nueva Empresa','Brown 814','(0291)4531704','Bahia Blanca');