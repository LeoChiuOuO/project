-- --------------------------------------------------------
-- 主機:                           127.0.0.1
-- 伺服器版本:                        11.7.2-MariaDB - mariadb.org binary distribution
-- 伺服器作業系統:                      Win64
-- HeidiSQL 版本:                  12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 傾印 dianthus_report 的資料庫結構
CREATE DATABASE IF NOT EXISTS `dianthus_report` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_uca1400_ai_ci */;
USE `dianthus_report`;

-- 傾印  資料表 dianthus_report.consult_records 結構
CREATE TABLE IF NOT EXISTS `consult_records` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '諮詢記錄編號',
  `report_id` int(11) NOT NULL COMMENT '對應報告 ID',
  `name` varchar(60) NOT NULL COMMENT '諮詢者姓名',
  `content` text NOT NULL COMMENT '諮詢內容',
  `created_at` datetime DEFAULT current_timestamp() COMMENT '建立時間',
  `updated_at` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '更新時間',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除時間',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `fk_consult_report` (`report_id`) USING BTREE,
  CONSTRAINT `fk_consult_report` FOREIGN KEY (`report_id`) REFERENCES `reports` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci COMMENT='諮詢記錄表';

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.department 結構
CREATE TABLE IF NOT EXISTS `department` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '部門編號',
  `name` varchar(20) NOT NULL COMMENT '部門名稱',
  `partition_id` int(11) NOT NULL COMMENT '區域編號',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`),
  KEY `partition_id` (`partition_id`),
  CONSTRAINT `department_ibfk_1` FOREIGN KEY (`partition_id`) REFERENCES `partition` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.group 結構
CREATE TABLE IF NOT EXISTS `group` (
  `id` int(11) NOT NULL COMMENT '組別編號',
  `name` varchar(20) NOT NULL COMMENT '組名',
  `department_id` int(11) NOT NULL COMMENT '部門編號',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`),
  KEY `department_id` (`department_id`),
  CONSTRAINT `group_ibfk_1` FOREIGN KEY (`department_id`) REFERENCES `department` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.operationlog 結構
CREATE TABLE IF NOT EXISTS `operationlog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL DEFAULT 0,
  `user_name` varchar(100) DEFAULT NULL,
  `ip_address` varchar(50) DEFAULT NULL,
  `action_type` varchar(50) DEFAULT NULL,
  `module` varchar(50) DEFAULT NULL,
  `success` tinyint(1) DEFAULT NULL,
  `description` text DEFAULT NULL,
  `created_at` datetime DEFAULT current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `idx_operationlog_userid` (`user_id`),
  KEY `idx_operationlog_createdat` (`created_at`)
) ENGINE=InnoDB AUTO_INCREMENT=1882 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.partition 結構
CREATE TABLE IF NOT EXISTS `partition` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '區域編號',
  `name` varchar(20) NOT NULL COMMENT '區域名稱',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.permissions 結構
CREATE TABLE IF NOT EXISTS `permissions` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '流水號',
  `name` varchar(20) DEFAULT NULL,
  `review_permissions` tinyint(1) DEFAULT 0 COMMENT '預覽權限',
  `create_permissions` tinyint(1) DEFAULT 0 COMMENT '新增權限',
  `edit_permissions` tinyint(1) DEFAULT 0 COMMENT '修改權限',
  `dele_permissions` tinyint(1) DEFAULT 0 COMMENT '刪除權限',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.reports 結構
CREATE TABLE IF NOT EXISTS `reports` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '報告編號',
  `report_id` varchar(30) DEFAULT NULL COMMENT '檢驗報告序號',
  `medical_order` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '醫令',
  `consent_form_state` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '同意書簽核狀態',
  `specimen_dely_state` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '檢體傳送狀態',
  `send_email_state` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT '未發送' COMMENT '發信狀態',
  `product_name` varchar(30) DEFAULT NULL COMMENT '檢驗產品名稱',
  `tracking_status` varchar(20) DEFAULT '待追蹤' COMMENT '追蹤狀態',
  `notification_status` varchar(20) DEFAULT '待通知' COMMENT '通知狀態',
  `partition_id` int(11) NOT NULL COMMENT '公司',
  `department_id` int(11) DEFAULT NULL COMMENT '單位',
  `group_id` int(11) DEFAULT NULL COMMENT '組',
  `submission_date` datetime DEFAULT NULL COMMENT '送檢日期',
  `received_date` date DEFAULT NULL COMMENT '收件日期',
  `name` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '姓名',
  `id_number` varchar(20) NOT NULL COMMENT '身分證',
  `birthday` datetime NOT NULL COMMENT '生日',
  `mr_number` int(11) NOT NULL COMMENT '病歷號',
  `test_item_id` int(11) DEFAULT NULL COMMENT '檢測項目 ID',
  `cost` int(11) DEFAULT 0 COMMENT '費用',
  `return_date` datetime DEFAULT NULL COMMENT '回診日期',
  `sending_physician_name` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '送檢醫師',
  `remark` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '附註',
  `report_date` datetime DEFAULT NULL COMMENT '報告日期',
  `report_results` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '報告結果',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `specimen_number` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '檢體編號',
  `specimen_type` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '檢體類別',
  `testing_date` datetime DEFAULT NULL COMMENT '採檢日期',
  `inspection_progress` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '檢驗進度',
  `assessment_status` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '評估狀態',
  `weeks_of_pregnancy` int(11) DEFAULT NULL COMMENT '懷孕週數',
  `due_date` datetime DEFAULT NULL COMMENT '預產期',
  `report_due_date` datetime DEFAULT NULL COMMENT '報告截止日期',
  `inspection_institution` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '送檢院所',
  `inspection_institution_phone` int(11) DEFAULT NULL COMMENT '送檢院所電話',
  `responsible_business_person` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '負責業務',
  `responsible_business_phone` int(11) DEFAULT NULL COMMENT '負責業務電話',
  `responsible_business_email` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '負責業務Email',
  `business_manager` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '業務主管',
  `business_manager_phone` int(11) DEFAULT NULL COMMENT '業務主管電話',
  `business_manager_email` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '業務主管Email',
  `abnormal_report_delivery_method` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '異常報告寄送方式',
  `abnormal_report_notification_method` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '異常報告通知方式',
  `inspection_group` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '檢驗組別',
  `notification_circumstances` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '通知情形',
  `prenatal_testing_project_tracking_time` datetime DEFAULT NULL COMMENT '產前檢測項目追蹤時間',
  `confirm_specimen_submission_time` datetime DEFAULT NULL COMMENT 'Confirm檢體進件時間',
  `confirm_specimen_type` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT 'Confirm檢體類別',
  `confirm_the_test_report_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT 'Confirm檢體報告結果',
  `tracking_time` datetime DEFAULT NULL COMMENT '追蹤時間',
  `tracking` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '追蹤情形',
  `tracking_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '追蹤結果',
  `tracking_the_followup_status_of_NIPS_cases` datetime DEFAULT NULL COMMENT '追蹤NIPS個案後續狀況時間',
  `referral_institution` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '轉介院所',
  `referring_physician` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '轉介醫師',
  `written_report_processing_methood` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '書面報告處理方式',
  `fmr1_report_results` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT 'FMR1結果',
  `chr_report_date` datetime NOT NULL COMMENT '染色體報告日期',
  `chr_report_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '染色體報告結果',
  `wafer_report_date` datetime NOT NULL COMMENT '晶片報告日期',
  `wafer_report_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '晶片結果',
  `v2_v3_testing_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT 'V2.0+V3.0基因結果',
  `gene_report_date` datetime NOT NULL COMMENT '基因檢測報告日期',
  `gene_report_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '基因檢測報告結果',
  `other_report_date` datetime NOT NULL COMMENT '其他檢測報告日期',
  `other_report_results` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '其他檢測報告結果',
  `created_at` datetime DEFAULT current_timestamp() COMMENT '建立日期',
  `updated_at` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`),
  KEY `fk_reports_test_item` (`test_item_id`),
  KEY `FK_Report_Group` (`group_id`),
  CONSTRAINT `FK_Report_Group` FOREIGN KEY (`group_id`) REFERENCES `group` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT `fk_reports_test_item` FOREIGN KEY (`test_item_id`) REFERENCES `test_item` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=119 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci COMMENT='報告總表';

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.roles 結構
CREATE TABLE IF NOT EXISTS `roles` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '角色編號',
  `name` varchar(30) NOT NULL COMMENT '角色名稱',
  `description` varchar(60) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '角色描述',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.role_permissions 結構
CREATE TABLE IF NOT EXISTS `role_permissions` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '流水號',
  `role_id` int(11) NOT NULL COMMENT '角色編號',
  `partition_id` int(11) NOT NULL COMMENT '區域編號',
  `department_id` int(11) NOT NULL COMMENT '部門編號',
  `group_id` int(11) NOT NULL COMMENT '組編號',
  `permissions_id` int(11) NOT NULL COMMENT '權限編號',
  `create_id` int(11) DEFAULT NULL COMMENT '建立者編號',
  `modify_id` int(11) DEFAULT NULL COMMENT '修改人員',
  `created_at` datetime NOT NULL COMMENT '建立日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`),
  KEY `role_id` (`role_id`),
  KEY `permissions_id` (`permissions_id`),
  CONSTRAINT `role_permissions_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`),
  CONSTRAINT `role_permissions_ibfk_2` FOREIGN KEY (`permissions_id`) REFERENCES `permissions` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.role_user 結構
CREATE TABLE IF NOT EXISTS `role_user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '對應編號',
  `user_id` int(11) NOT NULL COMMENT '使用者編號',
  `role_id` int(11) NOT NULL COMMENT '角色編號',
  `created_at` datetime NOT NULL COMMENT '建立時間',
  `updated_at` datetime NOT NULL COMMENT '修改時間',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除時間',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `role_id` (`role_id`),
  CONSTRAINT `role_user_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `role_user_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.test_item 結構
CREATE TABLE IF NOT EXISTS `test_item` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '項目編號',
  `name` varchar(20) NOT NULL COMMENT '項目名稱',
  `created_at` datetime DEFAULT current_timestamp() COMMENT '建立日期',
  `updated_at` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci COMMENT='檢測項目';

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.tracking_timeline 結構
CREATE TABLE IF NOT EXISTS `tracking_timeline` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '追蹤項目編號',
  `report_id` int(11) NOT NULL COMMENT '對應報告 ID',
  `name` varchar(60) NOT NULL COMMENT '追蹤對象姓名',
  `status` varchar(30) NOT NULL COMMENT '追蹤狀態',
  `created_at` datetime DEFAULT current_timestamp() COMMENT '建立時間',
  `updated_at` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp() COMMENT '更新時間',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除時間',
  PRIMARY KEY (`id`) USING BTREE,
  KEY `fk_tracking_report` (`report_id`) USING BTREE,
  CONSTRAINT `fk_tracking_report` FOREIGN KEY (`report_id`) REFERENCES `reports` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci COMMENT='追蹤時間軸表';

-- 取消選取資料匯出。

-- 傾印  資料表 dianthus_report.users 結構
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '使用者編號',
  `account` varchar(30) NOT NULL COMMENT '使用者帳號',
  `password` varchar(128) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci NOT NULL COMMENT '使用者密碼',
  `name` varchar(30) CHARACTER SET utf8mb3 COLLATE utf8mb3_uca1400_ai_ci DEFAULT NULL COMMENT '客戶名稱',
  `email` varchar(30) DEFAULT NULL COMMENT '電子信箱',
  `last_login_date` datetime DEFAULT NULL COMMENT '最後登入日期',
  `last_password_changed_date` datetime DEFAULT NULL COMMENT '最後修改密碼日期',
  `created_at` datetime NOT NULL COMMENT '申請日期',
  `updated_at` datetime NOT NULL COMMENT '修改日期',
  `deleted_at` datetime DEFAULT NULL COMMENT '刪除日期',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 取消選取資料匯出。

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
