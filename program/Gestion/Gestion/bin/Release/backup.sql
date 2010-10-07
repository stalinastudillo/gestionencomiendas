-- MySQL dump 10.13  Distrib 5.1.32, for Win32 (ia32)
--
-- Host: localhost    Database: gestion
-- ------------------------------------------------------
-- Server version	5.1.32-community

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `clientes` (
  `cuil` varchar(13) NOT NULL,
  `nombre` varchar(45) NOT NULL,
  `apellido` varchar(45) NOT NULL,
  `direccion` varchar(45) NOT NULL,
  `ciudad` varchar(45) NOT NULL,
  `codigo_postal` varchar(10) NOT NULL,
  `transporte` varchar(45) DEFAULT NULL,
  `telefono` varchar(25) DEFAULT NULL,
  `celular` varchar(30) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`cuil`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES ('20-20040194-9','Marcelo','Stupino','Alsina 467','Mar del Plata','7600',NULL,'4959058',NULL,NULL),('20-32246883-1','Jonathan','Vainstein','Zapiola 1147','Bahía Blanca','8000',NULL,'154427466',NULL,NULL),('20-33238584-9','Facundo','Santiago','Alvarado 766','Bahía Blanca','8000',NULL,'154554630',NULL,NULL),('20-33386905-6','Leonardo','Molas','Nicaragua 1243','Bahía Blanca','8000',NULL,'155028698',NULL,NULL),('20-33480673-2','Diego','Marcovecchio','Lamadrid 62 Torre B Entrepiso 1','Bahía Blanca','8000',NULL,'156424325',NULL,NULL),('20-33590251-4','Tomás','Touceda','Mitre 90 Piso 9 Dto C','Bahía Blanca','8000',NULL,'155730161',NULL,NULL),('20-36383277-7','Federico','Marcovecchio','Benito Juarez 349','Mar del Plata','7600',NULL,'4714200',NULL,NULL),('27-32838750-1','Perillo','Vanesa','Alem 1841','Bahía Blanca','8000',NULL,'155360362',NULL,NULL),('27-33177462-1','Estefanía','Lara','Belgrano 3784','Mar del Plata','7600',NULL,'155628626',NULL,NULL),('27-33558807-5','Eliana','Bega','Almafuerte 7680','Capital Federal','7200',NULL,'1556034156',NULL,NULL);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `factura`
--

DROP TABLE IF EXISTS `factura`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `factura` (
  `nro_factura` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `cuil` varchar(13) NOT NULL,
  `nombre` varchar(45) NOT NULL,
  `apellido` varchar(45) NOT NULL,
  `fecha` date NOT NULL,
  `iva` varchar(45) NOT NULL,
  `cond_venta` varchar(45) NOT NULL,
  `total` float(8,2) unsigned DEFAULT '0.00',
  PRIMARY KEY (`nro_factura`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `factura`
--

LOCK TABLES `factura` WRITE;
/*!40000 ALTER TABLE `factura` DISABLE KEYS */;
/*!40000 ALTER TABLE `factura` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prod_a_vender`
--

DROP TABLE IF EXISTS `prod_a_vender`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `prod_a_vender` (
  `codigo_prod` int(10) unsigned NOT NULL,
  `cantidad_prod` int(10) unsigned NOT NULL,
  `precio_prod` float(8,2) unsigned NOT NULL,
  PRIMARY KEY (`codigo_prod`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `prod_a_vender`
--

LOCK TABLES `prod_a_vender` WRITE;
/*!40000 ALTER TABLE `prod_a_vender` DISABLE KEYS */;
/*!40000 ALTER TABLE `prod_a_vender` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prod_vendidos`
--

DROP TABLE IF EXISTS `prod_vendidos`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `prod_vendidos` (
  `nro_factura` int(10) unsigned NOT NULL,
  `codigo` int(10) unsigned NOT NULL,
  `nombre` varchar(45) NOT NULL,
  `precio` float(8,2) unsigned NOT NULL,
  `cantidad` int(10) unsigned NOT NULL,
  PRIMARY KEY (`nro_factura`,`codigo`),
  KEY `FK_prod_vendidos_factura` (`nro_factura`),
  CONSTRAINT `FK_prod_vendidos_factura` FOREIGN KEY (`nro_factura`) REFERENCES `factura` (`nro_factura`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `prod_vendidos`
--

LOCK TABLES `prod_vendidos` WRITE;
/*!40000 ALTER TABLE `prod_vendidos` DISABLE KEYS */;
/*!40000 ALTER TABLE `prod_vendidos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `producto`
--

DROP TABLE IF EXISTS `producto`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `producto` (
  `codigo` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `precio` float(8,2) unsigned NOT NULL,
  `cantidad` int(10) unsigned NOT NULL,
  `descripcion` text NOT NULL,
  `proveedor` varchar(13) NOT NULL,
  PRIMARY KEY (`codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `producto`
--

LOCK TABLES `producto` WRITE;
/*!40000 ALTER TABLE `producto` DISABLE KEYS */;
INSERT INTO `producto` VALUES (1,'Camiseta Rosario Central',65.70,400,'Talles S, M, L, XL','27-30147486-0'),(2,'Camiseta Roma',189.00,600,'Talles S, M, L','27-30147486-0'),(3,'Camiseta GELP',65.00,200,'Talles S, M, L','27-30147486-0'),(4,'Camiseta River Plate',184.00,1200,'Talles S, M, L, XL','27-25039017-9'),(5,'Camiseta Real Madrid',264.90,300,'Talles S, M, L, XL, XXL','27-25039017-9'),(6,'Camiseta Selección Argentina 2009',140.00,4347,'Talles S, M, L, XL, XXL','27-25039017-9'),(7,'Camiseta Barcelona FC',247.80,300,'Todos los talles; Dorsal Etoo, Henry, Messi, Yaya Toure y Puyol','27-29257183-1'),(8,'Camiseta Boca Jrs.',184.00,906,'Talles S, M, L, XL','27-29257183-1'),(9,'Mouse óptico',31.50,60,'Mouse primera calidad s/ tecla programable','23-34500571-4'),(10,'Pad chico',21.30,90,'Color negro o azul','23-34500571-4'),(11,'Impresora HPD-645 Series',211.75,10,'Impresora blanco y negro/color','27-31821200-2'),(12,'Impresora HPD-670 Series',290.75,41,'Impresora blanco y negro/color','27-31821200-2'),(13,'Impresora LaserJet CP1000',411.75,22,'Impresora laser','27-31821200-2'),(14,'Impresora LaserJet CP3500',471.75,10,'Impresora laser','27-31821200-2'),(15,'Gabinete Kramer negro',121.30,11,'Con display externo de temperatura en GPU','20-22895691-1'),(16,'Gabinete Triton negro/plateado',120.10,13,'Con display externo de temperatura en GPU','20-22895691-1'),(17,'Set parlantes 2.1 chicos',60.00,29,'Aceptable calidad sonora','20-22895691-1'),(18,'Set parlantes 2.1 medianos',90.00,29,'Buena calidad sonora','20-22895691-1'),(19,'Set parlantes 5.1 c/sw',143.99,18,'Excelente calidad sonora','20-22895691-1');
/*!40000 ALTER TABLE `producto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proveedor`
--

DROP TABLE IF EXISTS `proveedor`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `proveedor` (
  `cuit` varchar(13) NOT NULL,
  `nombre` varchar(45) NOT NULL,
  `direccion` varchar(45) NOT NULL,
  `codigo_postal` varchar(45) NOT NULL,
  `ciudad` varchar(45) NOT NULL,
  `provincia` varchar(45) NOT NULL,
  `telefono` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `website` varchar(45) DEFAULT NULL,
  `descripcion` text,
  PRIMARY KEY (`cuit`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `proveedor`
--

LOCK TABLES `proveedor` WRITE;
/*!40000 ALTER TABLE `proveedor` DISABLE KEYS */;
INSERT INTO `proveedor` VALUES ('20-22895691-1','EURO','Figueroa Alcorta 566','7200','Capital Federal','Buenos Aires',NULL,NULL,NULL,NULL),('23-34500571-4','Genius','Yrigoyen 808','7600','Mar del Plata','Buenos Aires',NULL,NULL,NULL,NULL),('27-25039017-9','Adidas','Roca 1722','7200','Capital Federal','Buenos Aires',NULL,NULL,NULL,NULL),('27-29257183-1','Nike','San Martín 192','7200','Capital Federal','Buenos Aires',NULL,NULL,NULL,NULL),('27-30147486-0','Kappa','Roca 1722','2000','Rosario','Santa Fe',NULL,NULL,NULL,NULL),('27-31821200-2','Hewlett-Packard','San Martín 2423','7600','Mar del Plata','Buenos Aires',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `proveedor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ultimafecha`
--

DROP TABLE IF EXISTS `ultimafecha`;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
CREATE TABLE `ultimafecha` (
  `fecha` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`fecha`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
SET character_set_client = @saved_cs_client;

--
-- Dumping data for table `ultimafecha`
--

LOCK TABLES `ultimafecha` WRITE;
/*!40000 ALTER TABLE `ultimafecha` DISABLE KEYS */;
INSERT INTO `ultimafecha` VALUES ('03-04-2009');
/*!40000 ALTER TABLE `ultimafecha` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2009-04-03 18:53:19
