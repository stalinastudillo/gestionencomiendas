create database gestionAntares;


USE gestionAntares;


create table remitosaLiquidar(

	idremito int(30) not null

) Engine=InnoDB;


create table empresa(

	idempresa int(30) unsigned not null auto_increment,
	nombre varchar(45) default null,
	direccion varchar(45) default null,
	telefono varchar(45) default null,
	ciudad varchar(45) default null,
	
	primary key  (idempresa)

) Engine=InnoDB;


create table factura(

	idfactura int(30) unsigned not null auto_increment,
	fechaCreacion     date default null,
	fechaFirst     date default null,
	fechaLast     date default null,
	total  float(8,2) unsigned default null,
	flete float(8,2) unsigned default null,
	seguro float(8,2) unsigned default null,
	descripcion Text default null,
	ivari float(8,2) unsigned default null,
	neto float(8,2) unsigned default null,
	credito float(8,2) unsigned default null,
	cond_vta varchar(45) default null,
	idcliente varchar(45) default null ,
	tipo varchar(20) default null ,
	iduser varchar(45) default null ,
	pagado boolean default false,
	primary key  (idfactura)
	
) Engine=InnoDB;


create table remito(

    idremito    int(30) unsigned not null auto_increment,	
    fecha     date default null,

    remitente   varchar(30) default null,
    domicilior  varchar(45) default null,
    localidadr  varchar(30) default null,
    cuilr       varchar(15) default null,

    destinatario varchar(30) default null,
    domiciliod   varchar(45) default null,
    localidadd   varchar(30) default null,
    cuild        varchar(15) default null,

    cond_vta         varchar(20) default null,
    iva              varchar(30) default null,
    valor_declarado  float(8,2) unsigned default null,
    contrareembolso  float(8,2) unsigned default null,
    flete  float(8,2) unsigned default null,

    seguro float(8,2) unsigned default null,


    liquidado        varchar(1)  default null,	
    observacion      varchar(45) default null, 

    idcliente        int(10)     unsigned default null,
    nro_remito       varchar(10) default null,

    iduser           int(6) not null, 

    total  float(8,2) unsigned default null,

    idfactura  int(30) unsigned,	

    primary key  (idremito)
  
) Engine=InnoDB;


create table  clientes (

    idcliente int(30) unsigned not null auto_increment,	
    cuil varchar(13) default null,
    nombre varchar(30) not null,
    apellido varchar(45) default null,
    direccion varchar(45) default null,
    ciudad varchar(45) default null,
    codigo_postal varchar(10) default null,
    telefono varchar(25) default null,
    celular varchar(30) default null,
    email varchar(45) default null,
    porcentaje_vd    float(8,2) unsigned default null,
    porcentaje_cr    float(8,2) unsigned default null,
    credito          float(8,2) unsigned default null,

    primary key  (idcliente)
  
) Engine=InnoDB;


create table  producto (

    codigo int(10) unsigned not null auto_increment,
    nombre varchar(45) not null,
    precio float(8,2) unsigned not null,
    idcliente int(10) unsigned not null,
    descripcion text default null,

    primary key  (codigo)

) Engine=InnoDB;


create table productos_vendidos (
			
    idprod_vendido int(30) unsigned not null auto_increment,
    nroprod_vendido int(30) unsigned,	
    idremito int(30) not null,
    cantidad int(30) default null,
    descripcion text default null,
    kg float(8,2) unsigned default null,
    precio float(8,2) unsigned default null,	

    primary key  (idprod_vendido)

 
) Engine=InnoDB;


create table productos_vender(

    codprod int(30) unsigned not null,
    cant int(30) default null,
    descr text default null,
    kgs float(8,2) unsigned default null,
    prec float(8,2) unsigned default null,
    
    primary key (codprod)

) Engine=InnoDB;


delimiter //
create procedure aumento_producto(in codigo_producto int(10) unsigned, in porcentaje float(8,4))
    begin
        declare cambio float(8,4) unsigned;
        
        if porcentaje > 0 then
            set cambio = (porcentaje / 100) + 1;
        else
            set cambio = 1 - ((porcentaje * (-1)) / 100);
        end if;
        
        update producto set precio = precio * cambio where (codigo = codigo_producto);
    end;
    //

    
create procedure crear_remito(in fecha date, in remitente varchar(30), in domicilior varchar(45), in localidadr varchar(30), in cuilr varchar(15), in destinatario varchar(30), in domiciliod varchar(45), in localidadd varchar(30), in cuild varchar(15), in cond_vta varchar(20), in iva varchar(30), in valor_declarado float(8,2), in contrareembolso float(8,2), in flete float(8,2), in seguro float(8,2),in observacion varchar(45), in idcliente int(10), in nro_remito varchar(10), in iduser int(6))
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
    end;
    //
    	

create procedure crear_factura(in fechaCreacion date,in fechaFirst date,in fechaLast date, in descripcion Text,in tipo varchar(20),in iduser varchar(45), in numFact int(30) unsigned)
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
	declare idfact int(30) unsigned;
    declare id int(30) unsigned;
	declare idc int(30) unsigned;
	declare condVenta varchar(45);
	

        declare cursor_remitos cursor for select * from remitosaLiquidar;
		declare cursor_condVta cursor for select cond_vta from remito where idremito=id;
        declare cursor_seguro cursor for select (seguro) from remito where (idremito=id);
		declare cursor_flete cursor for select (flete) from remito where (idremito=id);
        declare cursor_factura cursor for select idfactura from factura where(idfactura = (SELECT MAX(idfactura) from factura));
		declare cursor_cliente cursor for select idcliente from remito where(idremito=id);
		declare cursor_creditoCliente cursor for select credito from clientes where(idcliente=idc);
        declare continue handler for not found set MasRemitos = false;

        start transaction;
        
            insert into factura(idfactura,fechaCreacion,fechaFirst,FechaLast,descripcion,credito,tipo,iduser) values (numFact,fechaCreacion,fechaFirst,FechaLast,descripcion,0,tipo,iduser);
        
            open cursor_factura;
            fetch cursor_factura into idfact;
            close cursor_factura;
			
			

            
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

		 insert into pago_factura(idfactura,pagado,ibb,ganancias,suss,saldo) values(numFact,false,null,null,null,valorTotal);
        commit;
    end;
    //


delimiter ;
	insert into clientes (idcliente,nombre,apellido,porcentaje_vd,porcentaje_cr,credito ) values (1,'Usuario General','',1,1,0);
	insert into empresa (nombre,direccion,telefono,ciudad) values ('Antares S.A','Brown 814','(0291)4531704','Bahía Blanca');
	


