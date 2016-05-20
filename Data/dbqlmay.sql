/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50515
Source Host           : localhost:3306
Source Database       : dbqlmay

Target Server Type    : MYSQL
Target Server Version : 50515
File Encoding         : 65001

Date: 2012-06-07 13:32:51
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `baotri`
-- ----------------------------
DROP TABLE IF EXISTS `baotri`;
CREATE TABLE `baotri` (
  `ID` varchar(10) NOT NULL,
  `Ngay` datetime DEFAULT NULL,
  `MaMay` varchar(10) DEFAULT NULL,
  `Noidung` text,
  `NgayBD` date DEFAULT NULL,
  `NgayKT` date DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of baotri
-- ----------------------------
INSERT INTO baotri VALUES ('1', '2012-05-30 00:00:00', '3', '1213345454', null, null);
INSERT INTO baotri VALUES ('2', '2012-05-30 00:00:00', '3', 'ssssssccccc', null, null);
INSERT INTO baotri VALUES ('3', '2012-06-04 00:00:00', '4', 'ffsfsdf', null, null);
INSERT INTO baotri VALUES ('4', '2012-06-04 00:00:00', '5', 'cbccsgsdgd', null, null);

-- ----------------------------
-- Table structure for `cauhinh`
-- ----------------------------
DROP TABLE IF EXISTS `cauhinh`;
CREATE TABLE `cauhinh` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `STT` varchar(10) NOT NULL DEFAULT '',
  `MaTB` varchar(10) DEFAULT NULL,
  `Noidung` text,
  PRIMARY KEY (`ID`,`STT`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cauhinh
-- ----------------------------
INSERT INTO cauhinh VALUES ('', '0', '2', '512');
INSERT INTO cauhinh VALUES ('', '1', '4', 'Misumi');
INSERT INTO cauhinh VALUES ('3', '0', '2', '512');
INSERT INTO cauhinh VALUES ('3', '1', '4', 'Misumi');
INSERT INTO cauhinh VALUES ('4', '0', '2', '512');
INSERT INTO cauhinh VALUES ('4', '1', '4', '12215');
INSERT INTO cauhinh VALUES ('4', '2', '3', '8');
INSERT INTO cauhinh VALUES ('4', '3', '3', '8');
INSERT INTO cauhinh VALUES ('5', '0', '2', '2');
INSERT INTO cauhinh VALUES ('5', '1', '4', '4');
INSERT INTO cauhinh VALUES ('6', '0', '2', '512');
INSERT INTO cauhinh VALUES ('6', '1', '4', 'Misumi');

-- ----------------------------
-- Table structure for `cauhinhmauct`
-- ----------------------------
DROP TABLE IF EXISTS `cauhinhmauct`;
CREATE TABLE `cauhinhmauct` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `STT` varchar(10) NOT NULL DEFAULT '',
  `MaTB` varchar(10) DEFAULT NULL,
  `Noidung` text,
  PRIMARY KEY (`ID`,`STT`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cauhinhmauct
-- ----------------------------
INSERT INTO cauhinhmauct VALUES ('1', '0', '2', 'Ram');
INSERT INTO cauhinhmauct VALUES ('1', '1', '3', 'MainBoard');
INSERT INTO cauhinhmauct VALUES ('1', '2', '4', 'Chu?t');
INSERT INTO cauhinhmauct VALUES ('2', '0', '4', 'Chu?t');
INSERT INTO cauhinhmauct VALUES ('2', '1', '2', 'Ram');
INSERT INTO cauhinhmauct VALUES ('3', '0', '4', 'Chu?t');
INSERT INTO cauhinhmauct VALUES ('3', '1', '4', 'Chu?t');
INSERT INTO cauhinhmauct VALUES ('4', '0', '2', 'Ram');
INSERT INTO cauhinhmauct VALUES ('4', '1', '4', 'Chuột');
INSERT INTO cauhinhmauct VALUES ('6', '0', '2', '512');
INSERT INTO cauhinhmauct VALUES ('6', '1', '4', 'Misumi');
INSERT INTO cauhinhmauct VALUES ('7', '0', '2', '512');
INSERT INTO cauhinhmauct VALUES ('7', '1', '4', 'Misumi');

-- ----------------------------
-- Table structure for `cauhinhmaull`
-- ----------------------------
DROP TABLE IF EXISTS `cauhinhmaull`;
CREATE TABLE `cauhinhmaull` (
  `ID` varchar(10) NOT NULL,
  `Ten` text,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of cauhinhmaull
-- ----------------------------
INSERT INTO cauhinhmaull VALUES ('1', '', '1');
INSERT INTO cauhinhmaull VALUES ('2', 'Mau s? 1', '1');
INSERT INTO cauhinhmaull VALUES ('3', 'th? m?u 1', '1');
INSERT INTO cauhinhmaull VALUES ('4', 'mẫu số 2', '1');
INSERT INTO cauhinhmaull VALUES ('5', '', '1');
INSERT INTO cauhinhmaull VALUES ('6', 'Mau 2', null);
INSERT INTO cauhinhmaull VALUES ('7', 'Mau 2', null);
INSERT INTO cauhinhmaull VALUES ('8', '', null);

-- ----------------------------
-- Table structure for `dmkhoaphong`
-- ----------------------------
DROP TABLE IF EXISTS `dmkhoaphong`;
CREATE TABLE `dmkhoaphong` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmkhoaphong
-- ----------------------------
INSERT INTO dmkhoaphong VALUES ('', null, null);
INSERT INTO dmkhoaphong VALUES ('1', 'Khoa Khám Bệnh', '1');
INSERT INTO dmkhoaphong VALUES ('2', 'Khoa Tim Mạch', '1');

-- ----------------------------
-- Table structure for `dmloaichitiet`
-- ----------------------------
DROP TABLE IF EXISTS `dmloaichitiet`;
CREATE TABLE `dmloaichitiet` (
  `ID` varchar(10) NOT NULL,
  `Ten` text,
  `LoaiMay` varchar(10) DEFAULT NULL,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmloaichitiet
-- ----------------------------
INSERT INTO dmloaichitiet VALUES ('1', 'Khác', null, '0');
INSERT INTO dmloaichitiet VALUES ('2', 'PC Pentium 4', null, '1');
INSERT INTO dmloaichitiet VALUES ('3', 'PC Core i3', null, '1');

-- ----------------------------
-- Table structure for `dmloaimay`
-- ----------------------------
DROP TABLE IF EXISTS `dmloaimay`;
CREATE TABLE `dmloaimay` (
  `ID` varchar(10) NOT NULL,
  `Ten` text,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmloaimay
-- ----------------------------
INSERT INTO dmloaimay VALUES ('1', 'Máy vi tính', '1');
INSERT INTO dmloaimay VALUES ('2', 'Máy in', '1');

-- ----------------------------
-- Table structure for `dmquocgia`
-- ----------------------------
DROP TABLE IF EXISTS `dmquocgia`;
CREATE TABLE `dmquocgia` (
  `ID` varchar(10) NOT NULL,
  `Ten` text,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmquocgia
-- ----------------------------
INSERT INTO dmquocgia VALUES ('1', 'Việt Nam', '1');
INSERT INTO dmquocgia VALUES ('2', 'Trung Quốc', '1');
INSERT INTO dmquocgia VALUES ('3', 'Nhật', '1');
INSERT INTO dmquocgia VALUES ('4', 'Mỹ', '1');
INSERT INTO dmquocgia VALUES ('5', 'Thái Lan', '1');

-- ----------------------------
-- Table structure for `dmthietbi`
-- ----------------------------
DROP TABLE IF EXISTS `dmthietbi`;
CREATE TABLE `dmthietbi` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text,
  `DVT` text,
  `CauHinh` text,
  `DonGia` int(11) DEFAULT NULL,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmthietbi
-- ----------------------------
INSERT INTO dmthietbi VALUES ('1', 'CPU', 'Cái', 'Ghz', null, '1');
INSERT INTO dmthietbi VALUES ('2', 'Ram', 'Thanh', 'GB', null, '1');
INSERT INTO dmthietbi VALUES ('3', 'MainBoard', 'Cái', '.', null, '1');
INSERT INTO dmthietbi VALUES ('4', 'Chuột', 'Cái', '.', null, '1');
INSERT INTO dmthietbi VALUES ('5', 'Bàn phím', 'Cái', '.', null, '0');
INSERT INTO dmthietbi VALUES ('6', 'Monitor', null, null, null, '1');
INSERT INTO dmthietbi VALUES ('7', 'CD-DVD', null, null, null, '1');

-- ----------------------------
-- Table structure for `dmtinhtrang`
-- ----------------------------
DROP TABLE IF EXISTS `dmtinhtrang`;
CREATE TABLE `dmtinhtrang` (
  `ID` varchar(10) NOT NULL,
  `Ten` text,
  `Enable` varchar(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dmtinhtrang
-- ----------------------------
INSERT INTO dmtinhtrang VALUES ('1', 'Đang sử dụng', '1');
INSERT INTO dmtinhtrang VALUES ('2', 'Đang sửa chữa', '1');
INSERT INTO dmtinhtrang VALUES ('3', 'Mới', '1');
INSERT INTO dmtinhtrang VALUES ('4', 'Hủy', '1');

-- ----------------------------
-- Table structure for `lichsumayct`
-- ----------------------------
DROP TABLE IF EXISTS `lichsumayct`;
CREATE TABLE `lichsumayct` (
  `ID` varchar(10) NOT NULL,
  `stt` varchar(10) NOT NULL,
  `MaThietBi` varchar(10) DEFAULT NULL,
  `NoiDung` text,
  PRIMARY KEY (`ID`,`stt`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of lichsumayct
-- ----------------------------

-- ----------------------------
-- Table structure for `lichsumayll`
-- ----------------------------
DROP TABLE IF EXISTS `lichsumayll`;
CREATE TABLE `lichsumayll` (
  `ID` varchar(10) NOT NULL,
  `Ngay` date DEFAULT NULL,
  `MaMay` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of lichsumayll
-- ----------------------------

-- ----------------------------
-- Table structure for `lichsuvitri`
-- ----------------------------
DROP TABLE IF EXISTS `lichsuvitri`;
CREATE TABLE `lichsuvitri` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ngay` date DEFAULT NULL,
  `KhoaPhongCu` varchar(10) DEFAULT NULL,
  `KhoaPhongMoi` varchar(10) DEFAULT NULL,
  `Vitri` text,
  `MaMay` varchar(50) NOT NULL DEFAULT '',
  `Nguoichuyen` text,
  PRIMARY KEY (`ID`,`MaMay`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of lichsuvitri
-- ----------------------------

-- ----------------------------
-- Table structure for `may`
-- ----------------------------
DROP TABLE IF EXISTS `may`;
CREATE TABLE `may` (
  `ID` varchar(10) NOT NULL,
  `MaMay` text,
  `Ten` text,
  `NuocSX` varchar(10) DEFAULT NULL,
  `MaKP` varchar(10) DEFAULT NULL,
  `MaLoai` varchar(10) DEFAULT NULL,
  `LoaiChiTiet` varchar(10) DEFAULT NULL,
  `DonGia` int(11) DEFAULT NULL,
  `Vitri` text,
  `Tinhtrang` varchar(10) DEFAULT NULL,
  `NgayBDSD` date DEFAULT NULL,
  `NgayUD` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of may
-- ----------------------------
INSERT INTO may VALUES ('01', '1', 'Máy vi tính', '1', '1', null, null, null, null, null, null, '2012-05-10 09:35:28');
INSERT INTO may VALUES ('02', '2', 'Máy In', '1', '1', null, null, null, null, null, null, '2012-05-19 09:35:35');
INSERT INTO may VALUES ('3', '02554', 'Máy vi tính ', '5', '1', '1', '2', '8000000', 'Hành chánh', '1', '2012-06-04', '2012-06-04 00:00:00');
INSERT INTO may VALUES ('4', '1255', 'Máy in lazer 14', '2', '2', '1', '3', '8000000', 'P13', '1', '2012-06-04', '2012-06-04 00:00:00');
INSERT INTO may VALUES ('5', '44443', 'hoan thanh', '1', '2', '2', '2', '50000', 'bbbb', '1', '2012-06-01', '2012-06-01 00:00:00');
INSERT INTO may VALUES ('6', '1152', 'Kb16', '1', '2', '1', '2', '411111', 'Kb16', '1', '2012-06-06', '2012-06-06 00:00:00');
