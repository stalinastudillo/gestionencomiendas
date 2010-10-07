USE gestionAntares;

create table pago_factura(

   idfactura int(45) not null,
   pagado boolean default false,
   ibb float(8,2) unsigned default null,
   ganancias float(8,2) unsigned default null,
   suss float(8,2) unsigned default null,
   saldo float(8,2) unsigned default null,

   primary key (idfactura)

)Engine=InnoDB;

create table pago_cheque(

   idcheque int(45) unsigned not null auto_increment,
   nro_cheque int(45) default null,
   banco  varchar(45) default null,
   monto float(8,2) unsigned default null,
   nro_recibo int(45) default null,
   fecha date default null,
   
   idfactura int(45) not null,

   primary key (idcheque)

) Engine=InnoDB;

create table pago_contado(

   idcontado int(45) unsigned not null auto_increment,
   fecha date default null,
   nro_recibo int(45) default null,
   monto float(8,2) unsigned default null,

   idfactura int(45) not null,

   primary key (idcontado)

) Engine=InnoDB;

drop procedure crear_factura;
delimiter //
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