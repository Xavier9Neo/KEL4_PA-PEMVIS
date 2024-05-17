-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 17, 2024 at 03:16 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `harrypotter`
--

-- --------------------------------------------------------

--
-- Table structure for table `barang`
--

CREATE TABLE `barang` (
  `idbarang` int(11) NOT NULL,
  `namabarang` text NOT NULL,
  `stokbarang` text NOT NULL,
  `hargabarang` text NOT NULL,
  `tanggal` text NOT NULL,
  `id_supplier` int(11) NOT NULL,
  `keterangan` text NOT NULL,
  `status` text NOT NULL,
  `tanggal_keluar` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `barang`
--

INSERT INTO `barang` (`idbarang`, `namabarang`, `stokbarang`, `hargabarang`, `tanggal`, `id_supplier`, `keterangan`, `status`, `tanggal_keluar`) VALUES
(45, 'Jubah Gaib', '20', '560000', '2024-05-16 14:38:29 ', 1, 'rusak', 'Ditolak', '2024-05-17'),
(47, 'Time Turner Necklace', '8', '999000000', 'Sunday, 31 March 2024', 0, '', '', NULL),
(65, 'Hogwarts Snow Globe', '13', '470000', 'Sunday, 31 March 2024', 0, '', '', NULL),
(78, 'Elder Wand', '65', '900000', '2024-05-17 10:13:07 ', 4, '', 'Diterima', '0000-00-00'),
(90, 'Deluminator', '2', '38410000', 'Sunday, 31 March 2024', 0, '', '', NULL),
(123, 'Time Turner Necklace', '60', '10000000', '2024-05-13 11:27:06 ', 4, '', '', '0000-00-00'),
(331, 'Jubah Gaib', '4', '529000', 'Sunday, 31 March 2024', 0, '', '', NULL),
(756, 'Elder Wand', '24', '23000', '2024-05-16 14:39:00 ', 3, '', '', NULL),
(965, 'Figure Rony Mcchlaney', '98', '890000', '2024-04-16 15:30:35 ', 2, '', '', NULL),
(966, 'Tongkat Ajaib', '6', '90000', '2024-05-17 14:33:27 ', 4, '', '', NULL),
(967, 'Sapu terbang', '38', '190000', '2024-05-16 14:43:05 ', 8, '', '', NULL),
(968, 'Miniature', '21', '90000', '2024-05-14 15:49:17 ', 8, '', '', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `login`
--

CREATE TABLE `login` (
  `Password` text NOT NULL,
  `Username` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `login`
--

INSERT INTO `login` (`Password`, `Username`) VALUES
('011', 'lko'),
('0909', 'dwiki'),
('098', 'tes'),
('1', 't'),
('1102', 'gagamaru'),
('123', 'wiki'),
('190', 'kakarotto'),
('2408', 'nona'),
('45', 'agss'),
('890', 'jaka'),
('98776', 'kkkk'),
('aaa', 'aaa'),
('b89', 'jajang'),
('bn', 'bn'),
('d', 'd'),
('edy', 'Edy'),
('h', 'hahaha'),
('haha', 'haha'),
('hkk', 'hkk'),
('hula', 'hula'),
('i\r\ni', 'i'),
('in', 'in'),
('ink', 'ink'),
('jagoan', 'jagoan'),
('jk', 'jk'),
('jn', 'jn'),
('kki', 'kki'),
('kkm', 'kkm'),
('kkv', 'kkv'),
('kll', 'kll'),
('kml', 'kml'),
('l', 'l'),
('mamat', 'mamat'),
('mm', 'nn'),
('n', 'n'),
('nano', 'nano'),
('nm', 'b'),
('qw', 'qw'),
('rembo', 'rembo'),
('ux', 'ui'),
('xx', 'x');

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier` (
  `id_supplier` int(11) NOT NULL,
  `nama_supplier` varchar(50) NOT NULL,
  `alamat` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`id_supplier`, `nama_supplier`, `alamat`) VALUES
(1, 'Lucius Malfoy', 'Malfoy Manor'),
(4, 'Professor Snape', 'Spinner\'s End'),
(8, 'Albus Dumbledore', 'Hogwarts');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`idbarang`),
  ADD KEY `supplier_id` (`id_supplier`);

--
-- Indexes for table `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`Password`(10));

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`id_supplier`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `barang`
--
ALTER TABLE `barang`
  MODIFY `idbarang` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=969;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
