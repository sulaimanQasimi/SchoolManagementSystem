// API Configuration
export const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000';

// App Configuration
export const APP_NAME = 'School Management System';

// Roles
export const ROLES = {
  ADMIN: 'Admin',
  TEACHER: 'Teacher',
  STUDENT: 'Student',
  PARENT: 'Parent'
};

// Pagination
export const DEFAULT_PAGE_SIZE = 10;
export const PAGE_SIZE_OPTIONS = [5, 10, 15, 20, 50];

// Date formats
export const DATE_FORMAT = 'YYYY-MM-DD';
export const DATE_TIME_FORMAT = 'YYYY-MM-DD HH:mm:ss';
export const DATE_DISPLAY_FORMAT = 'MMM DD, YYYY';

// File upload
export const MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
export const ALLOWED_FILE_TYPES = {
  images: ['image/jpeg', 'image/png', 'image/gif'],
  documents: ['application/pdf', 'application/msword', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'],
  all: ['image/jpeg', 'image/png', 'image/gif', 'application/pdf', 'application/msword', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document']
};

// Dashboard charts
export const CHART_COLORS = {
  primary: '#4F46E5',
  secondary: '#9333EA',
  success: '#10B981',
  danger: '#EF4444',
  warning: '#F59E0B',
  info: '#3B82F6',
  light: '#F3F4F6',
  dark: '#111827'
}; 