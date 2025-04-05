import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import { useAuth } from './contexts/AuthContext';
import { useTheme } from './contexts/ThemeContext';

// Layouts
import DashboardLayout from './layouts/DashboardLayout';
import AuthLayout from './layouts/AuthLayout';

// Pages
import Dashboard from './pages/Dashboard';
import Login from './pages/auth/Login';
import Register from './pages/auth/Register';
import ForgotPassword from './pages/auth/ForgotPassword';
import ResetPassword from './pages/auth/ResetPassword';
import Profile from './pages/Profile';
import Students from './pages/students/Students';
import StudentDetails from './pages/students/StudentDetails';
import Teachers from './pages/teachers/Teachers';
import TeacherDetails from './pages/teachers/TeacherDetails';
import Parents from './pages/parents/Parents';
import ParentDetails from './pages/parents/ParentDetails';
import Classes from './pages/classes/Classes';
import ClassDetails from './pages/classes/ClassDetails';
import NotFound from './pages/NotFound';

// Protected route wrapper
const ProtectedRoute = ({ children, allowedRoles = [] }) => {
  const { user, isAuthenticated, isLoading } = useAuth();
  
  if (isLoading) {
    return <div className="flex justify-center items-center h-screen">Loading...</div>;
  }
  
  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }
  
  if (allowedRoles.length > 0 && !allowedRoles.includes(user.role)) {
    return <Navigate to="/dashboard" />;
  }
  
  return children;
};

function App() {
  const { darkMode } = useTheme();
  
  return (
    <div className={darkMode ? 'dark' : ''}>
      <Routes>
        {/* Auth routes */}
        <Route element={<AuthLayout />}>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/forgot-password" element={<ForgotPassword />} />
          <Route path="/reset-password" element={<ResetPassword />} />
        </Route>
        
        {/* Dashboard routes */}
        <Route element={
          <ProtectedRoute>
            <DashboardLayout />
          </ProtectedRoute>
        }>
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/profile" element={<Profile />} />
          
          {/* Admin and Teacher routes */}
          <Route path="/students" element={
            <ProtectedRoute allowedRoles={['Admin', 'Teacher']}>
              <Students />
            </ProtectedRoute>
          } />
          <Route path="/students/:id" element={
            <ProtectedRoute allowedRoles={['Admin', 'Teacher', 'Student', 'Parent']}>
              <StudentDetails />
            </ProtectedRoute>
          } />
          
          {/* Admin routes */}
          <Route path="/teachers" element={
            <ProtectedRoute allowedRoles={['Admin']}>
              <Teachers />
            </ProtectedRoute>
          } />
          <Route path="/teachers/:id" element={
            <ProtectedRoute allowedRoles={['Admin', 'Teacher']}>
              <TeacherDetails />
            </ProtectedRoute>
          } />
          <Route path="/parents" element={
            <ProtectedRoute allowedRoles={['Admin']}>
              <Parents />
            </ProtectedRoute>
          } />
          <Route path="/parents/:id" element={
            <ProtectedRoute allowedRoles={['Admin', 'Parent']}>
              <ParentDetails />
            </ProtectedRoute>
          } />
          <Route path="/classes" element={
            <ProtectedRoute allowedRoles={['Admin']}>
              <Classes />
            </ProtectedRoute>
          } />
          <Route path="/classes/:id" element={
            <ProtectedRoute allowedRoles={['Admin', 'Teacher']}>
              <ClassDetails />
            </ProtectedRoute>
          } />
        </Route>
        
        {/* Redirect from root to dashboard or login */}
        <Route path="/" element={
          <Navigate to="/dashboard" replace />
        } />
        
        {/* 404 Page */}
        <Route path="*" element={<NotFound />} />
      </Routes>
    </div>
  );
}

export default App; 