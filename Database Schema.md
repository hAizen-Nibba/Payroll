[payrolldb_test.sql](https://github.com/user-attachments/files/26641644/payrolldb_test.3.sql)
-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 11, 2026 at 02:07 AM
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
-- Database: `payrolldb_test`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `att_id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `work_date` date NOT NULL,
  `time_in` time NOT NULL,
  `time_out` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`att_id`, `emp_id`, `work_date`, `time_in`, `time_out`) VALUES
(1, 1, '2026-04-01', '08:00:00', '17:00:00'),
(2, 1, '2026-04-02', '08:05:00', '17:00:00'),
(3, 1, '2026-04-03', '08:00:00', '17:10:00'),
(4, 2, '2026-04-01', '08:00:00', '17:00:00'),
(5, 2, '2026-04-02', '08:10:00', '17:15:00'),
(6, 2, '2026-04-03', '08:00:00', '17:30:00'),
(7, 3, '2026-04-01', '08:05:00', '17:00:00'),
(8, 3, '2026-04-02', '08:00:00', '17:00:00'),
(9, 3, '2026-04-03', '08:15:00', '17:20:00'),
(10, 4, '2026-04-01', '08:00:00', '17:00:00'),
(11, 4, '2026-04-02', '08:00:00', '17:00:00'),
(12, 4, '2026-04-03', '08:05:00', '17:10:00'),
(13, 5, '2026-04-01', '08:20:00', '17:00:00'),
(14, 5, '2026-04-02', '08:00:00', '17:00:00'),
(15, 5, '2026-04-03', '08:00:00', '17:25:00'),
(16, 6, '2026-04-01', '08:00:00', '17:00:00'),
(17, 6, '2026-04-02', '08:12:00', '17:00:00'),
(18, 6, '2026-04-03', '08:00:00', '17:40:00'),
(19, 1, '2026-04-17', '17:30:00', '20:30:00');

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `dept_id` int(11) NOT NULL,
  `dept_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`dept_id`, `dept_name`) VALUES
(1, 'College Deptartment');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `emp_id` int(11) NOT NULL,
  `f_name` varchar(100) NOT NULL,
  `l_name` varchar(100) NOT NULL,
  `dept_id` int(11) NOT NULL,
  `pos_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`emp_id`, `f_name`, `l_name`, `dept_id`, `pos_id`) VALUES
(1, 'Juan', 'Dela Cruz', 1, 1),
(2, 'Maria', 'Santos', 1, 2),
(3, 'Pedro', 'Reyes', 1, 3),
(4, 'Ana', 'Garcia', 1, 4),
(5, 'Luis', 'Torres', 1, 5),
(6, 'Carla', 'Mendoza', 1, 6),
(10, 'Boy', 'Nigga', 1, 1),
(11, 'aa', 'aa', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `overtime`
--

CREATE TABLE `overtime` (
  `ot_id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `ot_date` date NOT NULL,
  `hours` decimal(5,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `overtime`
--

INSERT INTO `overtime` (`ot_id`, `emp_id`, `ot_date`, `hours`) VALUES
(1, 1, '2026-04-03', 2.00),
(2, 2, '2026-04-03', 1.50),
(3, 3, '2026-04-03', 1.00),
(4, 4, '2026-04-03', 0.50),
(5, 5, '2026-04-03', 1.50),
(6, 6, '2026-04-03', 2.00);

-- --------------------------------------------------------

--
-- Table structure for table `payroll_deduction_items`
--

CREATE TABLE `payroll_deduction_items` (
  `ded_id` int(11) NOT NULL,
  `pay_rec_id` int(11) NOT NULL,
  `ded_type` varchar(100) NOT NULL,
  `amount` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payroll_deduction_items`
--

INSERT INTO `payroll_deduction_items` (`ded_id`, `pay_rec_id`, `ded_type`, `amount`) VALUES
(1, 1, 'SSS', 200.00),
(2, 1, 'PhilHealth', 150.00),
(3, 1, 'Pag-IBIG', 150.00),
(4, 2, 'SSS', 200.00),
(5, 2, 'PhilHealth', 150.00),
(6, 2, 'Pag-IBIG', 150.00),
(7, 3, 'SSS', 200.00),
(8, 3, 'PhilHealth', 150.00),
(9, 3, 'Pag-IBIG', 150.00),
(10, 4, 'SSS', 200.00),
(11, 4, 'PhilHealth', 150.00),
(12, 4, 'Pag-IBIG', 150.00),
(13, 5, 'SSS', 200.00),
(14, 5, 'PhilHealth', 150.00),
(15, 5, 'Pag-IBIG', 150.00),
(16, 6, 'SSS', 200.00),
(17, 6, 'PhilHealth', 150.00),
(18, 6, 'Pag-IBIG', 150.00);

-- --------------------------------------------------------

--
-- Table structure for table `payroll_earning_items`
--

CREATE TABLE `payroll_earning_items` (
  `earning_id` int(11) NOT NULL,
  `pay_rec_id` int(11) NOT NULL,
  `earn_type` varchar(100) NOT NULL,
  `amount` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payroll_earning_items`
--

INSERT INTO `payroll_earning_items` (`earning_id`, `pay_rec_id`, `earn_type`, `amount`) VALUES
(1, 1, 'Basic Pay', 8500.00),
(2, 1, 'Overtime Pay', 500.00),
(3, 2, 'Basic Pay', 7000.00),
(4, 2, 'Overtime Pay', 500.00),
(5, 3, 'Basic Pay', 6500.00),
(6, 3, 'Overtime Pay', 500.00),
(7, 4, 'Basic Pay', 5500.00),
(8, 4, 'Overtime Pay', 750.00),
(9, 5, 'Basic Pay', 5000.00),
(10, 5, 'Overtime Pay', 1250.00),
(11, 6, 'Basic Pay', 5000.00),
(12, 6, 'Overtime Pay', 1500.00),
(13, 8, 'Basic Pay', 12500.00),
(14, 8, 'Overtime Pay', 0.00);

-- --------------------------------------------------------

--
-- Table structure for table `payroll_periods`
--

CREATE TABLE `payroll_periods` (
  `payroll_id` int(11) NOT NULL,
  `period_start` date NOT NULL,
  `period_end` date NOT NULL,
  `pay_date` date NOT NULL,
  `status` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payroll_periods`
--

INSERT INTO `payroll_periods` (`payroll_id`, `period_start`, `period_end`, `pay_date`, `status`) VALUES
(1, '2026-04-01', '2026-04-15', '2026-04-15', 'Pending'),
(2, '2026-04-16', '2026-04-30', '2026-04-30', 'Pending'),
(3, '2026-05-01', '2026-05-15', '2026-05-15', 'Pending'),
(4, '2026-05-16', '2026-05-31', '2026-05-31', 'Pending'),
(5, '2026-06-01', '2026-06-15', '2026-06-15', 'Pending'),
(6, '2026-06-16', '2026-06-30', '2026-06-30', 'Pending'),
(7, '2026-07-01', '2026-07-15', '2026-07-15', 'Pending'),
(8, '2026-07-16', '2026-07-31', '2026-07-31', 'Pending'),
(9, '2026-08-01', '2026-08-15', '2026-08-15', 'Pending'),
(10, '2026-08-16', '2026-08-31', '2026-08-31', 'Pending'),
(11, '2026-09-01', '2026-09-15', '2026-09-15', 'Pending'),
(12, '2026-09-16', '2026-09-30', '2026-09-30', 'Pending'),
(13, '2026-10-01', '2026-10-15', '2026-10-15', 'Pending'),
(14, '2026-10-16', '2026-10-31', '2026-10-31', 'Pending'),
(15, '2026-11-01', '2026-11-15', '2026-11-15', 'Pending'),
(16, '2026-11-16', '2026-11-30', '2026-11-30', 'Pending'),
(17, '2026-12-01', '2026-12-15', '2026-12-15', 'Pending'),
(18, '2026-12-16', '2026-12-31', '2026-12-31', 'Pending');

-- --------------------------------------------------------

--
-- Table structure for table `payroll_slip_record`
--

CREATE TABLE `payroll_slip_record` (
  `pay_rec_id` int(11) NOT NULL,
  `payroll_id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `gross_pay` decimal(12,2) NOT NULL,
  `total_deduction` decimal(12,2) NOT NULL,
  `net_pay` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payroll_slip_record`
--

INSERT INTO `payroll_slip_record` (`pay_rec_id`, `payroll_id`, `emp_id`, `gross_pay`, `total_deduction`, `net_pay`) VALUES
(1, 1, 1, 9000.00, 500.00, 8500.00),
(2, 1, 2, 7500.00, 500.00, 7000.00),
(3, 1, 3, 7000.00, 500.00, 6500.00),
(4, 1, 4, 6250.00, 500.00, 5750.00),
(5, 1, 5, 6250.00, 500.00, 5750.00),
(6, 1, 6, 6500.00, 500.00, 6000.00),
(7, 3, 1, 0.00, 0.00, 0.00),
(8, 2, 1, 12500.00, 0.00, 12500.00);

-- --------------------------------------------------------

--
-- Table structure for table `positions`
--

CREATE TABLE `positions` (
  `pos_id` int(11) NOT NULL,
  `pos_name` varchar(100) NOT NULL,
  `basic_salary` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `positions`
--

INSERT INTO `positions` (`pos_id`, `pos_name`, `basic_salary`) VALUES
(1, 'Dean', 25000.00),
(2, 'Program Head', 20000.00),
(3, 'Librarian', 18000.00),
(4, 'Instructor 1', 15000.00),
(5, 'Instructor 2', 14000.00),
(6, 'Instructor 3', 13000.00);

-- --------------------------------------------------------

--
-- Table structure for table `security`
--

CREATE TABLE `security` (
  `sec_id` int(11) NOT NULL,
  `emp_id` int(11) NOT NULL,
  `password` varchar(500) NOT NULL,
  `sec_quest` varchar(500) NOT NULL,
  `sec_ans` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `security`
--

INSERT INTO `security` (`sec_id`, `emp_id`, `password`, `sec_quest`, `sec_ans`) VALUES
(1, 1, 'f6ccb3e8d609012238c0b39e60b2c9632b3cdede91e035dad1de43469768f4cc', 'What is your favorite color?', 'Blue'),
(2, 2, '626e3c805e77eeb472c42c6be607be2af7ac5c08fd7050f278e0330fe81abf57', 'What is your favorite color?', 'Red'),
(3, 3, 'a0ca7f03f5315a8ab7346ee1f96efcc30b1fbe2c3f070029d21274bcd5e3df4b', 'What is your favorite color?', 'Green'),
(4, 4, 'd77003f7563dd85de8668516ff5ac258a24ca1f3ad0f54fca712d09d3d7d93b9', 'What is your favorite color?', 'Pink'),
(5, 5, '72d6e7f95d6b9154b33f7d89c6b1cbf6de6f0f7ac8e1877c3900ef3734fa8656', 'What is your favorite color?', 'Black'),
(6, 6, '85cb64df2893f42ec87bd33590a54fecc1f0541f741451f52cc745644d1a6a84', 'What is your favorite color?', 'White'),
(9, 10, '011d5bf5158db0137554ca7c15bce8618f0075352878b60bfe7c69f7a7eac907', 'pogi ba ako?', 'yes'),
(10, 11, 'ed02457b5c41d964dbd2f2a609d63fe1bb7528dbe55e1abf5b52c249cd735797', 'aa', 'aa');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`att_id`),
  ADD KEY `attendance_fk1` (`emp_id`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`dept_id`),
  ADD UNIQUE KEY `dept_id` (`dept_id`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`emp_id`),
  ADD UNIQUE KEY `emp_id` (`emp_id`),
  ADD KEY `employees_fk3` (`dept_id`),
  ADD KEY `employees_fk4` (`pos_id`);

--
-- Indexes for table `overtime`
--
ALTER TABLE `overtime`
  ADD PRIMARY KEY (`ot_id`),
  ADD KEY `overtime_fk1` (`emp_id`);

--
-- Indexes for table `payroll_deduction_items`
--
ALTER TABLE `payroll_deduction_items`
  ADD PRIMARY KEY (`ded_id`),
  ADD KEY `payroll_deduction_items_fk1` (`pay_rec_id`);

--
-- Indexes for table `payroll_earning_items`
--
ALTER TABLE `payroll_earning_items`
  ADD PRIMARY KEY (`earning_id`),
  ADD KEY `payroll_earning_items_fk1` (`pay_rec_id`);

--
-- Indexes for table `payroll_periods`
--
ALTER TABLE `payroll_periods`
  ADD PRIMARY KEY (`payroll_id`),
  ADD UNIQUE KEY `payroll_id` (`payroll_id`);

--
-- Indexes for table `payroll_slip_record`
--
ALTER TABLE `payroll_slip_record`
  ADD PRIMARY KEY (`pay_rec_id`),
  ADD KEY `payroll_slip_record_fk1` (`payroll_id`),
  ADD KEY `payroll_slip_record_fk2` (`emp_id`);

--
-- Indexes for table `positions`
--
ALTER TABLE `positions`
  ADD PRIMARY KEY (`pos_id`),
  ADD UNIQUE KEY `pos_id` (`pos_id`);

--
-- Indexes for table `security`
--
ALTER TABLE `security`
  ADD PRIMARY KEY (`sec_id`),
  ADD UNIQUE KEY `sec_id` (`sec_id`),
  ADD KEY `security_fk1` (`emp_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `att_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `department`
--
ALTER TABLE `department`
  MODIFY `dept_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `emp_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `overtime`
--
ALTER TABLE `overtime`
  MODIFY `ot_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `payroll_deduction_items`
--
ALTER TABLE `payroll_deduction_items`
  MODIFY `ded_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `payroll_earning_items`
--
ALTER TABLE `payroll_earning_items`
  MODIFY `earning_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `payroll_periods`
--
ALTER TABLE `payroll_periods`
  MODIFY `payroll_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `payroll_slip_record`
--
ALTER TABLE `payroll_slip_record`
  MODIFY `pay_rec_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `positions`
--
ALTER TABLE `positions`
  MODIFY `pos_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `security`
--
ALTER TABLE `security`
  MODIFY `sec_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendance`
--
ALTER TABLE `attendance`
  ADD CONSTRAINT `attendance_fk1` FOREIGN KEY (`emp_id`) REFERENCES `employees` (`emp_id`);

--
-- Constraints for table `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `employees_fk3` FOREIGN KEY (`dept_id`) REFERENCES `department` (`dept_id`),
  ADD CONSTRAINT `employees_fk4` FOREIGN KEY (`pos_id`) REFERENCES `positions` (`pos_id`);

--
-- Constraints for table `overtime`
--
ALTER TABLE `overtime`
  ADD CONSTRAINT `overtime_fk1` FOREIGN KEY (`emp_id`) REFERENCES `employees` (`emp_id`);

--
-- Constraints for table `payroll_deduction_items`
--
ALTER TABLE `payroll_deduction_items`
  ADD CONSTRAINT `payroll_deduction_items_fk1` FOREIGN KEY (`pay_rec_id`) REFERENCES `payroll_slip_record` (`pay_rec_id`);

--
-- Constraints for table `payroll_earning_items`
--
ALTER TABLE `payroll_earning_items`
  ADD CONSTRAINT `payroll_earning_items_fk1` FOREIGN KEY (`pay_rec_id`) REFERENCES `payroll_slip_record` (`pay_rec_id`);

--
-- Constraints for table `payroll_slip_record`
--
ALTER TABLE `payroll_slip_record`
  ADD CONSTRAINT `payroll_slip_record_fk1` FOREIGN KEY (`payroll_id`) REFERENCES `payroll_periods` (`payroll_id`),
  ADD CONSTRAINT `payroll_slip_record_fk2` FOREIGN KEY (`emp_id`) REFERENCES `employees` (`emp_id`);

--
-- Constraints for table `security`
--
ALTER TABLE `security`
  ADD CONSTRAINT `security_fk1` FOREIGN KEY (`emp_id`) REFERENCES `employees` (`emp_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
