/*
Navicat MySQL Data Transfer

Source Server         : MYSQL
Source Server Version : 50515
Source Host           : localhost:3306
Source Database       : qlthebhyt

Target Server Type    : MYSQL
Target Server Version : 50515
File Encoding         : 65001

Date: 2013-06-04 11:19:21
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `bhyt_phieunhapll`
-- ----------------------------
DROP TABLE IF EXISTS `bhyt_phieunhapll`;
CREATE TABLE `bhyt_phieunhapll` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ngay` date DEFAULT NULL,
  `IDPhieu` varchar(10) DEFAULT NULL,
  `NoiDung` text,
  `Duyet` varchar(10) DEFAULT NULL,
  `NgayUD` date DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of bhyt_phieunhapll
-- ----------------------------

-- ----------------------------
-- Table structure for `dmhinhthucyc`
-- ----------------------------
DROP TABLE IF EXISTS `dmhinhthucyc`;
CREATE TABLE `dmhinhthucyc` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dmhinhthucyc
-- ----------------------------
INSERT INTO dmhinhthucyc VALUES ('0', 'Đúng tuyến', '1');
INSERT INTO dmhinhthucyc VALUES ('1', 'Trái tuyến', '1');

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
-- Table structure for `dmloaiyeucau`
-- ----------------------------
DROP TABLE IF EXISTS `dmloaiyeucau`;
CREATE TABLE `dmloaiyeucau` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dmloaiyeucau
-- ----------------------------
INSERT INTO dmloaiyeucau VALUES ('1', 'Đợt giao thứ  1', '1');
INSERT INTO dmloaiyeucau VALUES ('10', 'Đợt giao thứ  7', '1');
INSERT INTO dmloaiyeucau VALUES ('12', 'Đợt giao thứ  8', '1');
INSERT INTO dmloaiyeucau VALUES ('14', 'Đợt giao thứ  9', '1');
INSERT INTO dmloaiyeucau VALUES ('16', 'Đợt giao thứ 10', '1');
INSERT INTO dmloaiyeucau VALUES ('18', 'Đợt giao thứ 11', '1');
INSERT INTO dmloaiyeucau VALUES ('2', 'Đợt giao thứ  2', '1');
INSERT INTO dmloaiyeucau VALUES ('20', 'Đợt giao thứ 12', '1');
INSERT INTO dmloaiyeucau VALUES ('22', 'Đợt giao thứ 13', '1');
INSERT INTO dmloaiyeucau VALUES ('24', 'Đợt giao thứ 14', '1');
INSERT INTO dmloaiyeucau VALUES ('26', 'Đợt giao thứ 15', '1');
INSERT INTO dmloaiyeucau VALUES ('28', 'Đợt giao thứ 16', '1');
INSERT INTO dmloaiyeucau VALUES ('3', 'Đợt giao thứ  3', '1');
INSERT INTO dmloaiyeucau VALUES ('30', 'Đợt giao thứ 17', '1');
INSERT INTO dmloaiyeucau VALUES ('32', 'Đợt giao thứ 18', '1');
INSERT INTO dmloaiyeucau VALUES ('34', 'Đợt giao thứ 19', '1');
INSERT INTO dmloaiyeucau VALUES ('36', 'Đợt giao thứ 20', '1');
INSERT INTO dmloaiyeucau VALUES ('38', 'Đợt giao thứ 21', '1');
INSERT INTO dmloaiyeucau VALUES ('4', 'Đợt giao thứ  4', '1');
INSERT INTO dmloaiyeucau VALUES ('40', 'Đợt giao thứ 22', '1');
INSERT INTO dmloaiyeucau VALUES ('42', 'Đợt giao thứ 23', '1');
INSERT INTO dmloaiyeucau VALUES ('44', 'Đợt giao thứ 24', '1');
INSERT INTO dmloaiyeucau VALUES ('46', 'Đợt giao thứ 25', '1');
INSERT INTO dmloaiyeucau VALUES ('48', 'Đợt giao thứ 26', '1');
INSERT INTO dmloaiyeucau VALUES ('50', 'Đợt giao thứ 27', '1');
INSERT INTO dmloaiyeucau VALUES ('52', 'Đợt giao thứ 28', '1');
INSERT INTO dmloaiyeucau VALUES ('54', 'Đợt giao thứ 29', '1');
INSERT INTO dmloaiyeucau VALUES ('56', 'Đợt giao thứ 30', '1');
INSERT INTO dmloaiyeucau VALUES ('6', 'Đợt giao thứ  5', '1');
INSERT INTO dmloaiyeucau VALUES ('8', 'Đợt giao thứ  6', '1');

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
INSERT INTO dmtinhtrang VALUES ('1', 'Giấy chuyển viện bản chính', '1');
INSERT INTO dmtinhtrang VALUES ('10', 'Thẻ Bảo hiểm Y tế \r\n- Chứng minh Nhân dân\r\n- Giấy chuyển viện photo + Giấy hẹn', '1');
INSERT INTO dmtinhtrang VALUES ('2', 'Giấy chuyển viện photo + giấy hẹn', '1');
INSERT INTO dmtinhtrang VALUES ('3', 'Chứng minh nhân nhân', '1');
INSERT INTO dmtinhtrang VALUES ('4', 'Thẻ Bảo hiểm Y tế', '1');
INSERT INTO dmtinhtrang VALUES ('6', 'Thẻ Bảo hiểm Y tế\r\n- Chứng minh Nhân dân', '1');
INSERT INTO dmtinhtrang VALUES ('8', 'Thẻ Bảo hiểm Y tế \r\n- Chứng minh Nhân dân \r\n- Giấy chuyển viện bản chính', '1');

-- ----------------------------
-- Table structure for `dmuser`
-- ----------------------------
DROP TABLE IF EXISTS `dmuser`;
CREATE TABLE `dmuser` (
  `ID` varchar(10) CHARACTER SET utf8 NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dmuser
-- ----------------------------
INSERT INTO dmuser VALUES ('1', 'Nguyễn Vũ Minh Duy', '1');
INSERT INTO dmuser VALUES ('2', 'Nguyễn Minh Cường', '1');
INSERT INTO dmuser VALUES ('3', 'Lê Thị Phương Nam', '1');
INSERT INTO dmuser VALUES ('4', 'Vũ Hoàng Anh', '1');
INSERT INTO dmuser VALUES ('5', 'Vũ Hoàng Hà', '1');
INSERT INTO dmuser VALUES ('6', 'Đặng Văn Hưng', '1');
INSERT INTO dmuser VALUES ('7', 'Phan Thị Thảo', '1');
INSERT INTO dmuser VALUES ('8', 'Lê Văn Hiếu', '1');
INSERT INTO dmuser VALUES ('9', 'Đinh Thu Phương', '1');

-- ----------------------------
-- Table structure for `tiepnhanyc`
-- ----------------------------
DROP TABLE IF EXISTS `tiepnhanyc`;
CREATE TABLE `tiepnhanyc` (
  `ID` varchar(10) NOT NULL,
  `MaBN` varchar(8) DEFAULT NULL,
  `HoTen` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `LoaiYeuCau` varchar(10) DEFAULT NULL,
  `NoiDung` text CHARACTER SET utf8,
  `KhoaPhong` varchar(10) DEFAULT NULL,
  `NguoiYeuCau` text CHARACTER SET utf8,
  `NguoiNhanYC` varchar(10) DEFAULT NULL,
  `HinhThuc` varchar(10) DEFAULT NULL,
  `ThoiGianYC` datetime DEFAULT NULL,
  `ThoiGianKT` datetime DEFAULT NULL,
  `NgayUD` datetime DEFAULT NULL,
  `TinhTrang` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of tiepnhanyc
-- ----------------------------
INSERT INTO tiepnhanyc VALUES ('11', '09003174', 'NGUYỄN THỊ THÚY (1969)', '1', '- Giấy chuyển viện bản chính\n- Chứng minh nhân nhân\n- Giấy chuyển viện photo + giấy hẹn\n', '1', 'HC779210030001579024', '3', '0', '2013-05-31 11:17:05', '2013-05-31 11:17:05', '2013-05-31 11:17:05', '2');
INSERT INTO tiepnhanyc VALUES ('13', '09024105', 'NGUYỄN HỒNG PHONG (1982)', '1', '', '1', 'DN701100110157779024', '3', '0', '2013-05-31 11:17:31', '2013-05-31 11:17:31', '2013-05-31 11:17:31', '3');
INSERT INTO tiepnhanyc VALUES ('15', '11668324', 'PHẠM NGỌC MAI (1952)', '1', '- Thẻ Bảo hiểm Y tế\n- Giấy chuyển viện bản chính\n', '1', 'GD789115040141489011', '3', '1', '2013-05-31 15:40:33', '2013-05-31 15:40:33', '2013-05-31 15:40:33', '1');
INSERT INTO tiepnhanyc VALUES ('17', '08063789', 'NGUYỄN THỊ HẠNH (1942)', '1', '- Giấy chuyển viện bản chính\n', '1', 'HT279120840062579024', '3', '0', '2013-05-31 15:55:44', '2013-05-31 15:55:44', '2013-05-31 15:55:44', '1');
INSERT INTO tiepnhanyc VALUES ('19', '08063789', 'NGUYỄN THỊ HẠNH (1942)', '1', '\n', '1', 'HT279120840062579024', '3', '0', '2013-05-31 15:56:34', '2013-05-31 15:56:34', '2013-05-31 15:56:34', '1');
INSERT INTO tiepnhanyc VALUES ('21', '11140516', 'VÕ VĂN CỨNG (1948)', '1', '', '1', 'GD787110420113187147', '3', '1', '2013-06-03 09:36:20', '2013-06-03 09:36:20', '2013-06-03 09:36:20', '3');
INSERT INTO tiepnhanyc VALUES ('25', '13092264', 'BÙI VĂN THỊNH (1972)', '2', '', '1', 'GD766090050023266006', '3', '1', '2013-06-03 09:57:17', '2013-06-03 09:57:17', '2013-06-03 09:57:17', '3');
INSERT INTO tiepnhanyc VALUES ('27', '11140516', 'VÕ VĂN CỨNG (1948)', '1', '- Chứng minh nhân nhân\n- Giấy chuyển viện bản chính\n', '1', 'GD787110420113187147', '3', '1', '2013-06-03 11:35:27', '2013-06-03 11:35:27', '2013-06-03 11:35:27', '1');
INSERT INTO tiepnhanyc VALUES ('29', '08012726', 'NGUYỄN THỊ NHAN (1958)', '1', '- Thẻ Bảo hiểm Y tế + Chứng minh Nhân dân + Giấy chuyển viện photo + Giấy hẹn\n', '1', 'DN779030180001779024', '3', '0', '2013-06-04 11:06:15', '2013-06-04 11:06:15', '2013-06-04 11:06:15', '10');
INSERT INTO tiepnhanyc VALUES ('31', '77777777', 'NGUYỄN VŨ MINH DUY (1930)', '1', '- Thẻ Bảo hiểm Y tế \n- Chứng minh Nhân dân \n- Giấy chuyển viện bản chính\n', '1', '', '3', '0', '2013-06-04 11:16:47', '2013-06-04 11:16:47', '2013-06-04 11:16:47', '8');
