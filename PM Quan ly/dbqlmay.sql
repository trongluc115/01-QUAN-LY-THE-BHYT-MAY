/*
Navicat MySQL Data Transfer

Source Server         : MYSQL
Source Server Version : 50515
Source Host           : localhost:3306
Source Database       : dbqlmay

Target Server Type    : MYSQL
Target Server Version : 50515
File Encoding         : 65001

Date: 2013-06-03 11:45:00
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
INSERT INTO baotri VALUES ('5', '2012-06-12 00:00:00', '4', 'ghcvhcghcc', null, null);
INSERT INTO baotri VALUES ('6', '2012-06-12 00:00:00', '4', 'asdfasjdas\ndsd\ndsd\na\n\n\nSADS\nDSdas\nDSD\n', null, null);

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
INSERT INTO cauhinh VALUES ('10', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('10', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('10', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('10', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('10', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('10', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('10', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('12', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('12', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('12', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('12', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('12', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('12', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('12', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('14', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('14', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('14', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('14', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('14', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('14', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('14', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('16', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('16', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('16', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('16', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('16', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('16', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('16', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('18', '0', '3', 'ASUS P4P800-MX');
INSERT INTO cauhinh VALUES ('18', '1', '1', 'P4 CPU 2.4 GHZ');
INSERT INTO cauhinh VALUES ('18', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('18', '3', '7', 'NO');
INSERT INTO cauhinh VALUES ('18', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('18', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('18', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('20', '0', '3', 'ASUS P5GCMX');
INSERT INTO cauhinh VALUES ('20', '1', '1', 'Dual core E2140 1.6Ghz ');
INSERT INTO cauhinh VALUES ('20', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('20', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('20', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('20', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('20', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('22', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('22', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('22', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('22', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('22', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('22', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('22', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('24', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinh VALUES ('24', '1', '1', 'Dual core E750 2.93Ghz');
INSERT INTO cauhinh VALUES ('24', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('24', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('24', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('24', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('24', '6', '8', '160GB');
INSERT INTO cauhinh VALUES ('26', '0', '3', 'ASUS P5L-MX');
INSERT INTO cauhinh VALUES ('26', '1', '1', 'E2140 1.60 Ghz');
INSERT INTO cauhinh VALUES ('26', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('26', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('26', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('26', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('26', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('28', '0', '3', 'GIGABYTE 945GZM S2');
INSERT INTO cauhinh VALUES ('28', '1', '1', 'P4 3.00Ghz');
INSERT INTO cauhinh VALUES ('28', '2', '2', '512MB');
INSERT INTO cauhinh VALUES ('28', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('28', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('28', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('28', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('3', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinh VALUES ('3', '1', '1', 'Dual core E750 2.93Ghz');
INSERT INTO cauhinh VALUES ('3', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('3', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('3', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('3', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('3', '6', '8', '160GB');
INSERT INTO cauhinh VALUES ('30', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('30', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('30', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('30', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('30', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('30', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('30', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('32', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('32', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('32', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('32', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('32', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('32', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('32', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('34', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('34', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('34', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('34', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('34', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('34', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('34', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('36', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('36', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('36', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('36', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('36', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('36', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('36', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('38', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('38', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('38', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('38', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('38', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('38', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('38', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('4', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('4', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('4', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('4', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('4', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('4', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('40', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('40', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('40', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('40', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('40', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('40', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('40', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('42', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinh VALUES ('42', '1', '1', 'Dual core E7500 2.93Ghz');
INSERT INTO cauhinh VALUES ('42', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('42', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('42', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('42', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('42', '6', '8', '160GB');
INSERT INTO cauhinh VALUES ('46', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('46', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('46', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('46', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('46', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('46', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('46', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('48', '0', '3', 'ASUS P4P800-MX');
INSERT INTO cauhinh VALUES ('48', '1', '1', 'P4 2.40Ghz');
INSERT INTO cauhinh VALUES ('48', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('48', '3', '7', 'NO');
INSERT INTO cauhinh VALUES ('48', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('48', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('48', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('5', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('5', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('5', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('5', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('5', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('5', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('5', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('52', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('52', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('52', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('52', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('52', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('52', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('52', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('54', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('54', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('54', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('54', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('54', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('54', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('54', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('56', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinh VALUES ('56', '1', '1', 'Dual core E750 2.93Ghz');
INSERT INTO cauhinh VALUES ('56', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('56', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('56', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('56', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('56', '6', '8', '160GB');
INSERT INTO cauhinh VALUES ('58', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('58', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('58', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('58', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('58', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('58', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('58', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('6', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinh VALUES ('6', '1', '1', 'Dual core E7500 2.93Ghz');
INSERT INTO cauhinh VALUES ('6', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('6', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('6', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('6', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('6', '6', '8', '160GB');
INSERT INTO cauhinh VALUES ('60', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('60', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('60', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('60', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('60', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('60', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('60', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('7', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('7', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('7', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('7', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('7', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('7', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('7', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('8', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinh VALUES ('8', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinh VALUES ('8', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('8', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('8', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('8', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('8', '6', '8', '80GB');
INSERT INTO cauhinh VALUES ('9', '0', '3', 'GIGABYTE 945GZM S2L');
INSERT INTO cauhinh VALUES ('9', '1', '1', 'P4 3.00Ghz');
INSERT INTO cauhinh VALUES ('9', '2', '2', '1 GB');
INSERT INTO cauhinh VALUES ('9', '3', '7', 'Asus');
INSERT INTO cauhinh VALUES ('9', '4', '4', 'Mitsumi');
INSERT INTO cauhinh VALUES ('9', '5', '5', 'Mitsumi');
INSERT INTO cauhinh VALUES ('9', '6', '8', '80GB');

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
INSERT INTO cauhinhmauct VALUES ('2', '0', '3', 'GIGABYTE G31-ES2L');
INSERT INTO cauhinhmauct VALUES ('2', '1', '1', 'Dual core E750 2.93Ghz');
INSERT INTO cauhinhmauct VALUES ('2', '2', '2', '1 GB');
INSERT INTO cauhinhmauct VALUES ('2', '3', '7', 'Asus');
INSERT INTO cauhinhmauct VALUES ('2', '4', '4', 'Mitsumi');
INSERT INTO cauhinhmauct VALUES ('2', '5', '5', 'Mitsumi');
INSERT INTO cauhinhmauct VALUES ('2', '6', '8', '160GB');
INSERT INTO cauhinhmauct VALUES ('3', '0', '3', 'GIGABYTE 945GCM S2L');
INSERT INTO cauhinhmauct VALUES ('3', '1', '1', 'Dual core E2180 2.00Ghz');
INSERT INTO cauhinhmauct VALUES ('3', '2', '2', '1 GB');
INSERT INTO cauhinhmauct VALUES ('3', '3', '7', 'Asus');
INSERT INTO cauhinhmauct VALUES ('3', '4', '4', 'Mitsumi');
INSERT INTO cauhinhmauct VALUES ('3', '5', '5', 'Mitsumi');
INSERT INTO cauhinhmauct VALUES ('3', '6', '8', '80GB');

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
INSERT INTO cauhinhmaull VALUES ('2', 'GIGABYTE G31-ES2L', null);
INSERT INTO cauhinhmaull VALUES ('3', 'GIGABYTE 945GCM S2L', null);

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
INSERT INTO dmkhoaphong VALUES ('1', 'A1- Khoa Nội Tiêu Hoá -      NOITH', '1');
INSERT INTO dmkhoaphong VALUES ('10', 'A5- Khoa Nội Tiết  -      NOITIET', '1');
INSERT INTO dmkhoaphong VALUES ('11', 'B1a - Khoa Ngoại Thần Kinh Bệnh Lý - NgTKBL', '1');
INSERT INTO dmkhoaphong VALUES ('12', 'B1b - Khoa Ngoại Thần Kinh Chấn Thương  -    NGTKCT', '1');
INSERT INTO dmkhoaphong VALUES ('13', 'B2a- Khoa Ngoại Tổng Quát   -      NGTQ', '1');
INSERT INTO dmkhoaphong VALUES ('14', 'B2b- Khoa Ngoại Lồng Ngực Mạch Máu  -     NGLNMM', '1');
INSERT INTO dmkhoaphong VALUES ('15', 'B3a- Khoa Tai Mũi Họng   -      KTMH', '1');
INSERT INTO dmkhoaphong VALUES ('16', 'B3b- Khoa Mắt   -      KMAT', '1');
INSERT INTO dmkhoaphong VALUES ('17', 'B3c- Khoa Răng Hàm Mặt   -     KRHM', '1');
INSERT INTO dmkhoaphong VALUES ('18', 'B4m- Phòng Mổ - HS Ngoại (GMHS)    -       PM-GMHS', '1');
INSERT INTO dmkhoaphong VALUES ('19', 'B5- Khoa Ngoại Chấn Thương Chỉnh Hình   -      NGCTCH', '1');
INSERT INTO dmkhoaphong VALUES ('2', 'A1NT- Khoa Nội Tiêu Hoá (đt Ngoại Trú)-      NOITHNGT', '1');
INSERT INTO dmkhoaphong VALUES ('20', 'B5NT- Khoa Ngoại Chấn Thương Chỉnh Hình   -      NGCTCHNT', '1');
INSERT INTO dmkhoaphong VALUES ('21', 'B6- Khoa Y Học Thể Dục Thể Thao   -      KYHTT', '1');
INSERT INTO dmkhoaphong VALUES ('22', 'B7- Khoa Phẫu Thuật Tim Hở  -     KPTTH', '1');
INSERT INTO dmkhoaphong VALUES ('23', 'CĐT- Phòng Chỉ đạo tuyến', '1');
INSERT INTO dmkhoaphong VALUES ('24', 'Cxk-khoa Cơ Xương Khớp    -      KCXK', '1');
INSERT INTO dmkhoaphong VALUES ('25', 'Cxk-khoa Cơ Xương Khớp (đt Ngoại trú)    -      KCXKNT', '1');
INSERT INTO dmkhoaphong VALUES ('26', 'Đơn vị Nội Soi', '1');
INSERT INTO dmkhoaphong VALUES ('27', 'HCQT-Hành chính quản trị', '1');
INSERT INTO dmkhoaphong VALUES ('28', 'Hs- Khoa Hồi Sức Tích Cực - Chống Độc  -      KHSTCCĐ', '1');
INSERT INTO dmkhoaphong VALUES ('29', 'Khoa Chẩn Đoán Hình Ảnh', '1');
INSERT INTO dmkhoaphong VALUES ('3', 'A2a- Khoa Tim Mạch Can Thiệp -      TMCT', '1');
INSERT INTO dmkhoaphong VALUES ('30', 'Khoa Chống Nhiễm Khuẩn', '1');
INSERT INTO dmkhoaphong VALUES ('31', 'Khoa Dinh Dưỡng', '1');
INSERT INTO dmkhoaphong VALUES ('32', 'Khoa Dược', '1');
INSERT INTO dmkhoaphong VALUES ('33', 'Khoa Giải Phẫu Bệnh', '1');
INSERT INTO dmkhoaphong VALUES ('34', 'Khoa khám bệnh', '1');
INSERT INTO dmkhoaphong VALUES ('35', 'Khoa Thận Ngoại', '1');
INSERT INTO dmkhoaphong VALUES ('36', 'Khoa Thận Nhân Tạo   -      KTNT', '1');
INSERT INTO dmkhoaphong VALUES ('37', 'Khoa Thận Nội (đt Ngoại Trú)    -      TNngt', '1');
INSERT INTO dmkhoaphong VALUES ('38', 'Khoa Xét Nghiệm', '1');
INSERT INTO dmkhoaphong VALUES ('39', 'Khu Khám Theo Yêu Cầu', '1');
INSERT INTO dmkhoaphong VALUES ('4', 'A2b- Khoa Tim Mạch Tổng Quát -    TMTQ', '1');
INSERT INTO dmkhoaphong VALUES ('40', 'Phòng CNTT', '1');
INSERT INTO dmkhoaphong VALUES ('41', 'Phòng Điều Dưỡng', '1');
INSERT INTO dmkhoaphong VALUES ('42', 'Phòng VTTB-YT', '1');
INSERT INTO dmkhoaphong VALUES ('43', 'TCKT-Phòng Tài chính Kế Toán', '1');
INSERT INTO dmkhoaphong VALUES ('44', 'Tn- Khoa Thận Nội   -   THANNOI', '1');
INSERT INTO dmkhoaphong VALUES ('45', 'UB-Đơn Vị Ung Bướu (Ngoại trú )   -      KUBng', '1');
INSERT INTO dmkhoaphong VALUES ('5', 'A2c- Khoa Nhịp Tim Học -     NHIPTH', '1');
INSERT INTO dmkhoaphong VALUES ('6', 'A2d- Khoa Hồi Sức Tim Mạch -      HSTM', '1');
INSERT INTO dmkhoaphong VALUES ('7', 'A3a- Khoa Nội Thần Kinh Tổng Quát -     TKTQ', '1');
INSERT INTO dmkhoaphong VALUES ('8', 'A3b- Khoa Bệnh Lý Mạch Máu Não -    BLMMN', '1');
INSERT INTO dmkhoaphong VALUES ('9', 'A4- Khoa Bệnh Nhiệt Đới -        KBNĐ', '1');

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
INSERT INTO dmloaichitiet VALUES ('11', 'Cannon 3300', '2', '1');
INSERT INTO dmloaichitiet VALUES ('2', 'PC Pentium 4', '1', '1');
INSERT INTO dmloaichitiet VALUES ('3', 'PC Core i3', '1', '1');
INSERT INTO dmloaichitiet VALUES ('4', 'HP LaserJet 1160', '2', '1');
INSERT INTO dmloaichitiet VALUES ('5', 'HP LaserJet P2014', '2', '1');
INSERT INTO dmloaichitiet VALUES ('6', 'HP LaserJet 1006', '2', '1');
INSERT INTO dmloaichitiet VALUES ('7', 'PC Dual Core', '1', '1');
INSERT INTO dmloaichitiet VALUES ('9', 'Printer HP2014', '2', '1');

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
INSERT INTO dmthietbi VALUES ('3', 'Mainboard', 'Cái', '.', null, '1');
INSERT INTO dmthietbi VALUES ('4', 'Mouse', 'Cái', '.', null, '1');
INSERT INTO dmthietbi VALUES ('5', 'Keyboard', 'Cái', '.', null, '1');
INSERT INTO dmthietbi VALUES ('6', 'Monitor', null, null, null, '1');
INSERT INTO dmthietbi VALUES ('7', 'CD-DVD', null, null, null, '1');
INSERT INTO dmthietbi VALUES ('8', 'HDD', null, null, null, '1');

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
INSERT INTO may VALUES ('10', '2424', 'KB2424', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('12', '2408', 'KB2408', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('14', '2409', 'KB2409', '2', '34', '1', '2', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('16', '2410', 'KB2410', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('18', '2430', 'KB2430', '2', '34', '1', '2', '1', 'E102 (Thy Anh)', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('20', '2412', 'KB2412', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('22', '2407', 'KB2407', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('24', '2414', 'KB2414', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('26', '2415', 'KB2415', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('28', '2416', 'KB2416', '2', '34', '1', '7', '1', 'E102 (C.LONG)', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('3', '2413', 'KB2413', '2', '34', '1', '7', '8000000', '', '1', '2012-10-12', '2012-10-12 00:00:00');
INSERT INTO may VALUES ('30', '2417', 'KB2417', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('32', '2418', 'KB2418', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('34', '2427', 'KB2427', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('36', '2423', 'KB2404', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('38', '2421', 'KB2421', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('4', '2403', 'KB03', '2', '34', '1', '7', '8000000', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('40', '2422', 'KB2422', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('42', '2402', 'KB2402', '2', '34', '1', '7', '1', '', '1', '2012-10-12', '2012-10-12 00:00:00');
INSERT INTO may VALUES ('44', '2435', 'KB2435', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('46', '2425', 'KB2425DT', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('48', '2426', 'KB2426DT', '2', '34', '1', '2', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('5', '2400', 'KB00', '2', '34', '1', '7', '50000', 'E108', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('50', '-', '-', '2', '35', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('52', '2420', 'KB2420', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('54', '2428', 'KB2428', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('56', '2429', 'KB2429', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('58', '2431', 'KB2431', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('6', '2401', 'KB01', '2', '34', '1', '7', '411111', 'MẤT NGÀY 08.10.2012', '4', '2012-10-12', '2012-10-12 00:00:00');
INSERT INTO may VALUES ('60', '2419', 'KB2419', '2', '34', '1', '7', '1', 'E102 (LỰC)', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('7', '2404', 'KB2404', '2', '34', '1', '7', '120000', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('8', '2405', 'KB2405', '2', '34', '1', '7', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
INSERT INTO may VALUES ('9', '2406', 'KB2406', '2', '34', '1', '2', '1', '', '1', '2012-10-08', '2012-10-08 00:00:00');
