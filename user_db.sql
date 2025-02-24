-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 04, 2024 at 05:48 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `user_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE `transactions` (
  `id` int(11) NOT NULL,
  `amount` decimal(10,2) NOT NULL,
  `category` varchar(255) NOT NULL,
  `transaction_type` enum('Expense','Income') NOT NULL,
  `transaction_date` date NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `initial_balance` double DEFAULT NULL,
  `username` varchar(255) NOT NULL,
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `transactions`
--

INSERT INTO `transactions` (`id`, `amount`, `category`, `transaction_type`, `transaction_date`, `created_at`, `initial_balance`, `username`, `updated_at`) VALUES
(1, 5000.00, 'Salary', 'Income', '2024-12-03', '2024-12-03 14:02:49', NULL, 'Hachi', '2024-12-03 14:02:49'),
(2, 500.00, 'Food', 'Expense', '2024-12-03', '2024-12-03 14:03:06', NULL, 'Hachi', '2024-12-03 14:03:06'),
(3, 10000.00, 'Salary', 'Income', '2024-12-03', '2024-12-03 15:35:51', NULL, 'vincent', '2024-12-03 15:35:51'),
(4, 5000.00, 'Rent', 'Expense', '2024-12-03', '2024-12-03 15:36:02', NULL, 'vincent', '2024-12-03 15:36:02'),
(5, 2000.00, 'Utilities', 'Expense', '2024-12-04', '2024-12-03 17:29:58', NULL, 'Hachi', '2024-12-03 17:29:58'),
(6, 2500.00, 'Investments', 'Income', '2024-12-04', '2024-12-03 17:30:17', NULL, 'Hachi', '2024-12-03 17:30:17'),
(7, 2500.00, 'Utilities', 'Expense', '2024-12-04', '2024-12-03 17:30:30', NULL, 'Hachi', '2024-12-03 17:30:30');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `name`, `email`, `username`, `password`, `created_at`, `updated_at`) VALUES
(1, 'Johnfel', 'eharesj@gmail.com', 'Hachi', '032203', '2024-11-29 14:08:39', '2024-12-03 13:51:03'),
(2, 'paeng', 'johnfel', 'vincent', '1234', '2024-12-03 12:38:38', '2024-12-03 13:51:03');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idx_username` (`username`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `transactions`
--
ALTER TABLE `transactions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
