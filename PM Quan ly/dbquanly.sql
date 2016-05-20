/*
Navicat MySQL Data Transfer

Source Server         : MYSQL
Source Server Version : 50515
Source Host           : localhost:3306
Source Database       : dbquanly

Target Server Type    : MYSQL
Target Server Version : 50515
File Encoding         : 65001

Date: 2013-06-03 11:44:31
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `dlogin`
-- ----------------------------
DROP TABLE IF EXISTS `dlogin`;
CREATE TABLE `dlogin` (
  `ID` varchar(10) CHARACTER SET utf8 NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Matkhau` text,
  `_Right` varchar(50) DEFAULT NULL,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dlogin
-- ----------------------------
INSERT INTO dlogin VALUES ('1', 'admin', '@115', '1^2_1^2_2^3^4^5^6', '1');
INSERT INTO dlogin VALUES ('2', 'bhyt', 'bhyt115', '2_1', '1');
INSERT INTO dlogin VALUES ('3', 'vppk', 'vppk115', '2_2', '1');

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
INSERT INTO dmhinhthucyc VALUES ('', null, null);
INSERT INTO dmhinhthucyc VALUES ('1', 'Trực tiếp', '1');
INSERT INTO dmhinhthucyc VALUES ('2', 'Điện thoại', '1');

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
INSERT INTO dmloaiyeucau VALUES ('1', 'Xóa chỉ định', '1');
INSERT INTO dmloaiyeucau VALUES ('2', 'Sửa chữa', '1');
INSERT INTO dmloaiyeucau VALUES ('3', 'Lệch viện phí', '1');
INSERT INTO dmloaiyeucau VALUES ('4', 'Yêu cầu khác', '1');
INSERT INTO dmloaiyeucau VALUES ('6', 'In chỉ định', '1');

-- ----------------------------
-- Table structure for `dmloaiyeucau_`
-- ----------------------------
DROP TABLE IF EXISTS `dmloaiyeucau_`;
CREATE TABLE `dmloaiyeucau_` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dmloaiyeucau_
-- ----------------------------
INSERT INTO dmloaiyeucau_ VALUES ('1', 'Đợt giao thứ 1', '1');
INSERT INTO dmloaiyeucau_ VALUES ('2', 'Đợt giao thứ 2', '1');
INSERT INTO dmloaiyeucau_ VALUES ('3', 'Đợt giao thứ 3', '1');
INSERT INTO dmloaiyeucau_ VALUES ('4', 'Đợt giao thứ 4', '1');

-- ----------------------------
-- Table structure for `dmloaiyeucau_copy`
-- ----------------------------
DROP TABLE IF EXISTS `dmloaiyeucau_copy`;
CREATE TABLE `dmloaiyeucau_copy` (
  `ID` varchar(10) NOT NULL DEFAULT '',
  `Ten` text CHARACTER SET utf8,
  `Enable` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of dmloaiyeucau_copy
-- ----------------------------
INSERT INTO dmloaiyeucau_copy VALUES ('1', 'Xóa chỉ định', '0');
INSERT INTO dmloaiyeucau_copy VALUES ('2', 'Sửa chữa', '1');
INSERT INTO dmloaiyeucau_copy VALUES ('3', 'Lệch viện phí', '1');
INSERT INTO dmloaiyeucau_copy VALUES ('4', 'Yêu cầu khác', '1');

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
INSERT INTO dmtinhtrang VALUES ('1', 'Đúng hẹn', '1');
INSERT INTO dmtinhtrang VALUES ('2', 'Không đúng hẹn', '1');
INSERT INTO dmtinhtrang VALUES ('3', 'Chờ chỉnh', '1');
INSERT INTO dmtinhtrang VALUES ('4', 'Hủy', '1');

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
INSERT INTO dmuser VALUES ('7', 'Nguyễn Trọng Lực', '1');
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
INSERT INTO tiepnhanyc VALUES ('1', '11067340', 'TRẦN ĐÌNH TÚ (1964)', '3', 'Xóa cận lâm sàng', '1', 'Minh', '3', '1', '2013-05-30 10:58:22', '2013-05-30 10:58:22', '2013-05-30 10:58:22', '1');
INSERT INTO tiepnhanyc VALUES ('11', '09024105', 'NGUYỄN HỒNG PHONG (1982)', '1', '-Xóa chỉ định ngày 05/31/2013 Khám da liễu ( SL:1) - K/P:Pk Da Liễu P03 ( E120 )\n-Xóa chỉ định ngày 05/31/2013 Cortison ( SL:1) - K/P:Pk Da Liễu P03 ( E120 )\n', '6', 'Minh', '3', '1', '2013-05-31 15:07:35', '2013-05-31 15:07:35', '2013-05-31 15:07:35', '3');
INSERT INTO tiepnhanyc VALUES ('3', '77777777', 'NGUYỄN VŨ MINH DUY (1930)', '3', 'fdfsfsfsfsdd', '1', 'hoàng', '3', '1', '2013-05-30 11:00:46', '2013-05-30 11:00:46', '2013-05-30 11:00:46', '1');
INSERT INTO tiepnhanyc VALUES ('5', '08020088', 'HỒ THỊ XUÂN THẢO (1975)', '2', ' -Hủy chỉ định ngày 03/26/2013 Tên chỉ định: CR - CỘT SỘNG THẮT LƯNG T_N (L1 L5) ( SL:1) - K/P:Pk Vật Lý Trị Liệu\n -Hủy chỉ định ngày 03/26/2013 Tên chỉ định: Xoa bóp bằng máy ( SL:5) - K/P:Pk Vật Lý Trị Liệu', '5', 'Long', '3', '1', '2013-05-30 11:13:28', '2013-05-30 11:13:28', '2013-05-30 11:13:28', '3');
INSERT INTO tiepnhanyc VALUES ('7', '11067340', 'TRẦN ĐÌNH TÚ (1964)', '3', '\n -Hủy chỉ định ngày 03/26/2013 Tên chỉ định: CR - CỘT SỘNG THẮT LƯNG T_N (L1 L5) ( SL:1) - K/P:Pk Vật Lý Trị Liệu\n -Hủy chỉ định ngày 03/26/2013 Tên chỉ định: Xoa bóp bằng máy ( SL:5) - K/P:Pk Vật Lý Trị Liệu', '1', '', '3', '1', '2013-05-30 14:25:23', '2013-05-30 14:25:23', '2013-05-30 14:25:23', '3');
INSERT INTO tiepnhanyc VALUES ('9', '09003174', 'NGUYỄN THỊ THÚY (1969)', '6', '-In chỉ định ngày 05/31/2013 Creatinine máu ( SL:1) - K/P:Pk Da Liễu P03 ( E120 )\n-In chỉ định ngày 05/31/2013 Đường huyết ( SL:1) - K/P:Pk Da Liễu P03 ( E120 )\n', '1', 'Hoàng', '3', '1', '2013-05-31 15:06:40', '2013-05-31 15:06:40', '2013-05-31 15:06:40', '3');
