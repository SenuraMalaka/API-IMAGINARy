-- phpMyAdmin SQL Dump
-- version 4.8.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: Apr 24, 2018 at 05:19 AM
-- Server version: 10.1.31-MariaDB
-- PHP Version: 7.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `api_sen_db`
--

create database api_sen_db;
use api_sen_db;

--Senura Added The Above Create and Use DB Commands

-- --------------------------------------------------------

--
-- Table structure for table `developers`
--


CREATE TABLE `developers` (
  `did` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `developers`
--

INSERT INTO `developers` (`did`, `name`) VALUES
(1, 'sen'),
(2, 'dinuka'),
(3, 'Banuri'),
(4, 'Pramitha'),
(5, 'Sachith'),
(6, 'Yomali'),
(8, 'Someone'),
(9, 'Heran'),
(10, 'Derek'),
(11, 'Manoja');

-- --------------------------------------------------------

--
-- Table structure for table `devProject`
--

CREATE TABLE `devProject` (
  `dpid` int(11) NOT NULL,
  `did` int(11) NOT NULL,
  `pid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `devProject`
--

INSERT INTO `devProject` (`dpid`, `did`, `pid`) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 4, 1),
(4, 3, 2),
(5, 5, 3);

-- --------------------------------------------------------

--
-- Table structure for table `devProjectReports`
--

CREATE TABLE `devProjectReports` (
  `dpid` int(11) NOT NULL,
  `date` date NOT NULL,
  `hours` int(11) NOT NULL,
  `ot` int(11) DEFAULT NULL,
  `description` varchar(250) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `devProjectReports`
--

INSERT INTO `devProjectReports` (`dpid`, `date`, `hours`, `ot`, `description`) VALUES
(1, '2018-04-05', 9, 1, 'worked 9 hours'),
(1, '2018-04-22', 8, 0, 'worked 8 hours'),
(1, '2018-04-24', 9, 1, 'worked 9 hours'),
(1, '2018-06-21', 16, 8, 'worked 16 hours'),
(2, '2018-04-23', 7, 0, 'worked 7 hours'),
(2, '2018-04-24', 8, 0, 'worked 8 hours'),
(3, '2018-06-21', 16, 8, 'worked 16 hours'),
(3, '2018-06-22', 9, 1, 'worked 9 hours'),
(4, '2018-06-20', 9, 1, 'worked 9 hours'),
(4, '2018-06-21', 7, 0, 'worked 7 hours'),
(5, '2018-06-20', 10, 2, 'worked 10 hours'),
(5, '2018-06-21', 6, 0, 'worked 6 hours');

-- --------------------------------------------------------

--
-- Table structure for table `managers`
--

CREATE TABLE `managers` (
  `mid` int(11) NOT NULL,
  `name` varchar(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `managers`
--

INSERT INTO `managers` (`mid`, `name`) VALUES
(1, 'Dileepa'),
(2, 'Anura');

-- --------------------------------------------------------

--
-- Table structure for table `project`
--

CREATE TABLE `project` (
  `pid` int(11) NOT NULL,
  `name` varchar(60) NOT NULL,
  `mid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `project`
--

INSERT INTO `project` (`pid`, `name`, `mid`) VALUES
(1, 'Mobile Application', 1),
(2, 'Desktop Application', 2),
(3, 'Server apps', 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `developers`
--
ALTER TABLE `developers`
  ADD PRIMARY KEY (`did`);

--
-- Indexes for table `devProject`
--
ALTER TABLE `devProject`
  ADD PRIMARY KEY (`dpid`),
  ADD KEY `devProject_didFK` (`did`),
  ADD KEY `devProject_pidFK` (`pid`);

--
-- Indexes for table `devProjectReports`
--
ALTER TABLE `devProjectReports`
  ADD PRIMARY KEY (`dpid`,`date`);

--
-- Indexes for table `managers`
--
ALTER TABLE `managers`
  ADD PRIMARY KEY (`mid`);

--
-- Indexes for table `project`
--
ALTER TABLE `project`
  ADD PRIMARY KEY (`pid`),
  ADD KEY `project_midFK` (`mid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `developers`
--
ALTER TABLE `developers`
  MODIFY `did` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `devProject`
--
ALTER TABLE `devProject`
  MODIFY `dpid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `managers`
--
ALTER TABLE `managers`
  MODIFY `mid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `project`
--
ALTER TABLE `project`
  MODIFY `pid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `devProject`
--
ALTER TABLE `devProject`
  ADD CONSTRAINT `devProject_didFK` FOREIGN KEY (`did`) REFERENCES `developers` (`did`),
  ADD CONSTRAINT `devProject_pidFK` FOREIGN KEY (`pid`) REFERENCES `project` (`pid`);

--
-- Constraints for table `devProjectReports`
--
ALTER TABLE `devProjectReports`
  ADD CONSTRAINT `devPReports_dpid` FOREIGN KEY (`dpid`) REFERENCES `devProject` (`dpid`);

--
-- Constraints for table `project`
--
ALTER TABLE `project`
  ADD CONSTRAINT `project_midFK` FOREIGN KEY (`mid`) REFERENCES `managers` (`mid`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
