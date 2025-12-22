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

-- 正在傾印表格  dianthus_report.consult_records 的資料：~0 rows (近似值)

-- 正在傾印表格  dianthus_report.department 的資料：~7 rows (近似值)
INSERT INTO `department` (`id`, `name`, `partition_id`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, '民權', 1, 1, 1, '2025-09-08 11:36:32', '2025-09-08 11:36:32', NULL),
	(2, '懷寧', 1, 1, 1, '2025-09-08 11:36:32', '2025-09-08 11:36:32', NULL),
	(3, '桃園', 1, 1, 1, '2025-09-08 11:36:32', '2025-09-08 11:36:32', NULL),
	(4, '諮詢', 2, 1, 1, '2025-10-22 15:37:43', '2025-10-22 15:37:44', NULL),
	(5, '婦產科', 3, 1, 1, '2025-10-22 15:38:09', '2025-10-22 15:38:10', NULL),
	(6, '小兒科', 4, 1, 1, '2025-10-22 15:39:23', '2025-10-22 15:39:24', NULL),
	(7, '腸胃科', 5, 1, 1, '2025-10-22 15:39:56', '2025-10-22 15:39:56', NULL);

-- 正在傾印表格  dianthus_report.group 的資料：~5 rows (近似值)
INSERT INTO `group` (`id`, `name`, `department_id`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, '基因組', 1, 1, 1, '2025-09-08 11:37:14', '2025-09-08 11:37:14', NULL),
	(2, '染色體組', 1, 1, 1, '2025-09-08 11:37:14', '2025-09-08 11:37:14', NULL),
	(3, '北區業務', 2, 1, 1, '2025-09-08 11:37:14', '2025-09-08 11:37:14', NULL),
	(4, '南區業務', 2, 1, 1, '2025-09-08 11:37:14', '2025-09-08 11:37:14', NULL),
	(5, '行政支援', 3, 1, 1, '2025-09-08 11:37:14', '2025-09-08 11:37:14', NULL);



-- 正在傾印表格  dianthus_report.partition 的資料：~5 rows (近似值)
INSERT INTO `partition` (`id`, `name`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, '禾馨醫療', 1, 1, '2025-09-08 11:35:35', '2025-09-08 11:35:35', NULL),
	(2, '慧智基因', 1, 1, '2025-10-13 17:02:44', '2025-10-13 17:02:45', NULL),
	(3, '台北馬偕', 1, 1, '2025-09-08 11:35:35', '2025-09-08 11:35:35', NULL),
	(4, '新光', 1, 1, '2025-10-13 17:04:47', '2025-10-13 17:04:48', NULL),
	(5, '中榮', 1, 1, '2025-09-08 11:35:35', '2025-09-08 11:35:35', NULL);

-- 正在傾印表格  dianthus_report.permissions 的資料：~4 rows (近似值)
INSERT INTO `permissions` (`id`, `name`, `review_permissions`, `create_permissions`, `edit_permissions`, `dele_permissions`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, '測試01', 1, 1, 1, 1, 1, 1, '2025-09-08 11:28:35', '2025-10-21 19:39:53', NULL),
	(2, '測試02', 1, 0, 0, 0, 1, 1, '2025-09-08 11:28:35', '2025-10-22 23:05:31', NULL),
	(3, '測試03', 1, 0, 1, 0, 1, 1, '2025-09-08 11:28:35', '2025-09-16 22:02:21', NULL),
	(14, '測試員04', 1, 0, 0, 0, 1, 1, '2025-10-22 23:43:28', '2025-10-22 23:53:29', NULL),
	(16, '測試05', 0, 0, 0, 0, 1, 1, '2025-10-23 09:28:45', '2025-10-28 16:21:02', NULL);

-- 正在傾印表格  dianthus_report.reports 的資料：~114 rows (近似值)
INSERT INTO `reports` (`id`, `report_id`, `medical_order`, `consent_form_state`, `specimen_dely_state`, `send_email_state`, `product_name`, `tracking_status`, `notification_status`, `partition_id`, `department_id`, `group_id`, `submission_date`, `received_date`, `name`, `id_number`, `mr_number`, `test_item_id`, `cost`, `return_date`, `sending_physician_name`, `remark`, `report_date`, `report_results`, `create_id`, `modify_id`, `specimen_number`, `specimen_type`, `testing_date`, `inspection_progress`, `assessment_status`, `weeks_of_pregnancy`, `due_date`, `report_due_date`, `inspection_institution`, `inspection_institution_phone`, `responsible_business_person`, `responsible_business_phone`, `responsible_business_email`, `business_manager`, `business_manager_phone`, `business_manager_email`, `abnormal_report_delivery_method`, `abnormal_report_notification_method`, `inspection_group`, `notification_circumstances`, `prenatal_testing_project_tracking_time`, `confirm_specimen_submission_time`, `confirm_specimen_type`, `confirm_the_test_report_results`, `tracking_time`, `tracking`, `tracking_results`, `tracking_the_followup_status_of_NIPS_cases`, `referral_institution`, `referring_physician`, `written_report_processing_methood`, `fmr1_report_results`, `chr_report_date`, `chr_report_results`, `wafer_report_date`, `wafer_report_results`, `v2_v3_testing_results`, `gene_report_date`, `gene_report_results`, `other_report_date`, `other_report_results`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 'RPT00001', 'MO001', '未簽', '已送達', '已發送', '產品E', '不需追蹤', '待通知', 5, 2, NULL, '2025-05-03 00:00:00', '2025-02-01', '測試姓名1', 'X913826562', 1001, 21, 5832, '2025-10-24 00:00:00', '醫師1', '備註1', '2025-11-24 00:00:00', '異常', 3, 5, 'SP0001', '血液', '2024-10-30 00:00:00', '檢驗中', '待評估', 18, '2025-12-23 00:00:00', '2025-09-28 00:00:00', '中榮', 93926454, '業務1', 992481310, 'sales1@example.com', '主管1', 942517932, 'manager1@example.com', '快遞', 'Email', '組別5', '情形3', '2025-11-24 00:00:00', '2025-10-25 00:00:00', '羊水', '陰性', '2025-11-25 00:00:00', '不需追蹤', '正常', '2025-11-25 00:00:00', '長庚', '醫師48', '快遞', '陰性', '2025-11-25 00:00:00', '異常', '2025-11-25 00:00:00', '異常', '陰性', '2025-10-26 00:00:00', '異常', '2025-10-26 00:00:00', '異常', '2025-09-30 11:55:24', '2025-10-22 22:56:57', NULL)

-- 正在傾印表格  dianthus_report.roles 的資料：~3 rows (近似值)
INSERT INTO `roles` (`id`, `name`, `description`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 'Admin', '系統管理者', 1, 1, '2025-09-08 11:28:22', '2025-09-08 11:28:22', NULL),
	(2, 'LabTech', '檢驗人員', 1, 1, '2025-09-08 11:28:22', '2025-09-08 11:28:22', NULL),
	(3, 'Sales', '業務人員', 1, 1, '2025-09-08 11:28:22', '2025-09-08 11:28:22', NULL),
	(9, '諮詢人員01', '禾馨民權', NULL, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL);

-- 正在傾印表格  dianthus_report.role_permissions 的資料：~4 rows (近似值)
INSERT INTO `role_permissions` (`id`, `role_id`, `partition_id`, `department_id`, `group_id`, `permissions_id`, `create_id`, `modify_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 2, 2, 4, 0, 2, 0, NULL, '2025-10-22 15:22:52', '2025-10-22 15:22:52', NULL),
	(2, 1, 1, 1, 0, 1, 0, NULL, '2025-10-22 15:45:55', '2025-10-22 15:45:57', NULL),
	(4, 3, 3, 5, 0, 3, 0, 0, '2025-10-22 15:43:00', '2025-10-22 23:10:01', NULL),
	(12, 9, 3, 5, 0, 16, 0, NULL, '2025-10-23 14:31:26', '2025-10-23 14:31:26', NULL);

-- 正在傾印表格  dianthus_report.role_user 的資料：~4 rows (近似值)
INSERT INTO `role_user` (`id`, `user_id`, `role_id`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 1, 1, '2025-10-22 15:21:43', '2025-10-22 15:21:44', NULL),
	(2, 2, 2, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL),
	(4, 3, 3, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL),
	(12, 4, 9, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL);

-- 正在傾印表格  dianthus_report.test_item 的資料：~50 rows (近似值)
INSERT INTO `test_item` (`id`, `name`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, '檢測項目01', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(2, '檢測項目02', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(3, '檢測項目03', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(4, '檢測項目04', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(5, '檢測項目05', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(6, '檢測項目06', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(7, '檢測項目07', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(8, '檢測項目08', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(9, '檢測項目09', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(10, '檢測項目10', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(11, '檢測項目11', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(12, '檢測項目12', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(13, '檢測項目13', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(14, '檢測項目14', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(15, '檢測項目15', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(16, '檢測項目16', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(17, '檢測項目17', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(18, '檢測項目18', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(19, '檢測項目19', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(20, '檢測項目20', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(21, '檢測項目21', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(22, '檢測項目22', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(23, '檢測項目23', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(24, '檢測項目24', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(25, '檢測項目25', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(26, '檢測項目26', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(27, '檢測項目27', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(28, '檢測項目28', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(29, '檢測項目29', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(30, '檢測項目30', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(31, '檢測項目31', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(32, '檢測項目32', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(33, '檢測項目33', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(34, '檢測項目34', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(35, '檢測項目35', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(36, '檢測項目36', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(37, '檢測項目37', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(38, '檢測項目38', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(39, '檢測項目39', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(40, '檢測項目40', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(41, '檢測項目41', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(42, '檢測項目42', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(43, '檢測項目43', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(44, '檢測項目44', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(45, '檢測項目45', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(46, '檢測項目46', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(47, '檢測項目47', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(48, '檢測項目48', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(49, '檢測項目49', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL),
	(50, '檢測項目50', '2025-09-30 11:45:04', '2025-09-30 11:45:04', NULL);

-- 正在傾印表格  dianthus_report.users 的資料：~6 rows (近似值)
INSERT INTO `users` (`id`, `account`, `password`, `name`, `email`, `last_login_date`, `last_password_changed_date`, `created_at`, `updated_at`, `deleted_at`) VALUES
	(1, 'admin', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '管理員', NULL, '2025-12-22 09:56:23', NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL),
	(2, 'alice', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '艾莉絲汀', NULL, '2025-10-22 23:16:21', NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL),
	(3, 'bob', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '小明', NULL, NULL, NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL),
	(4, 'carol', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '佳佳', NULL, '2025-10-28 16:21:12', NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL),
	(5, 'david', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '大衛', NULL, NULL, NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL),
	(6, 'eva', '$2a$11$FXLFLFtW9X5jwldB69/F1uPYv/.imutcNcSWiNPcF3u4.Dn25wpP6', '怡君', NULL, NULL, NULL, '2025-09-08 11:20:44', '2025-09-08 11:20:44', NULL);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
