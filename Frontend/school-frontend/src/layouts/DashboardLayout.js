import React, { useState } from 'react';
import { Outlet, Link, useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { useTheme } from '../contexts/ThemeContext';
import { APP_NAME, ROLES } from '../config/constants';

// Icons (you would normally import from a library like react-icons)
const Icons = {
  Dashboard: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6" />
    </svg>
  ),
  Students: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
    </svg>
  ),
  Teachers: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
    </svg>
  ),
  Parents: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
    </svg>
  ),
  Classes: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
    </svg>
  ),
  Profile: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
    </svg>
  ),
  Logout: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
    </svg>
  ),
  Menu: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
    </svg>
  ),
  Close: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
    </svg>
  ),
  Sun: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 3v1m0 16v1m9-9h-1M4 12H3m15.364 6.364l-.707-.707M6.343 6.343l-.707-.707m12.728 0l-.707.707M6.343 17.657l-.707.707M16 12a4 4 0 11-8 0 4 4 0 018 0z" />
    </svg>
  ),
  Moon: () => (
    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20.354 15.354A9 9 0 018.646 3.646 9.003 9.003 0 0012 21a9.003 9.003 0 008.354-5.646z" />
    </svg>
  )
};

const NavLink = ({ to, label, icon, isSidebarOpen, onClick }) => {
  const location = useLocation();
  const isActive = location.pathname === to;

  return (
    <Link
      to={to}
      onClick={onClick}
      className={`flex items-center p-2 rounded-lg ${
        isActive
          ? 'bg-primary-100 text-primary-700 dark:bg-primary-900 dark:text-primary-200'
          : 'text-gray-600 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-800'
      } ${isSidebarOpen ? '' : 'justify-center'}`}
    >
      <span className="flex-shrink-0">{icon}</span>
      {isSidebarOpen && <span className="ml-3">{label}</span>}
    </Link>
  );
};

const DashboardLayout = () => {
  const { user, logout } = useAuth();
  const { darkMode, toggleDarkMode } = useTheme();
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const toggleSidebar = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  const toggleMobileMenu = () => {
    setIsMobileMenuOpen(!isMobileMenuOpen);
  };

  const closeMobileMenu = () => {
    setIsMobileMenuOpen(false);
  };

  // Define navigation items based on user role
  const getNavItems = () => {
    const navItems = [
      {
        to: '/dashboard',
        label: 'Dashboard',
        icon: <Icons.Dashboard />,
        allowedRoles: [ROLES.ADMIN, ROLES.TEACHER, ROLES.STUDENT, ROLES.PARENT]
      },
      {
        to: '/students',
        label: 'Students',
        icon: <Icons.Students />,
        allowedRoles: [ROLES.ADMIN, ROLES.TEACHER]
      },
      {
        to: '/teachers',
        label: 'Teachers',
        icon: <Icons.Teachers />,
        allowedRoles: [ROLES.ADMIN]
      },
      {
        to: '/parents',
        label: 'Parents',
        icon: <Icons.Parents />,
        allowedRoles: [ROLES.ADMIN]
      },
      {
        to: '/classes',
        label: 'Classes',
        icon: <Icons.Classes />,
        allowedRoles: [ROLES.ADMIN, ROLES.TEACHER]
      },
      {
        to: '/profile',
        label: 'Profile',
        icon: <Icons.Profile />,
        allowedRoles: [ROLES.ADMIN, ROLES.TEACHER, ROLES.STUDENT, ROLES.PARENT]
      }
    ];

    // Filter nav items based on user role
    if (!user || !user.role) return [];
    return navItems.filter(item => item.allowedRoles.includes(user.role));
  };

  return (
    <div className="flex h-screen bg-gray-100 dark:bg-gray-900">
      {/* Sidebar - Desktop */}
      <aside
        className={`fixed inset-y-0 left-0 z-20 transform ${
          isSidebarOpen ? 'w-64' : 'w-20'
        } bg-white dark:bg-gray-800 shadow-md transition-all duration-300 ease-in-out lg:translate-x-0 ${
          isMobileMenuOpen ? 'translate-x-0' : '-translate-x-full'
        } lg:relative`}
      >
        <div className="flex flex-col h-full">
          {/* Logo */}
          <div className={`flex items-center justify-between h-16 px-4 border-b border-gray-200 dark:border-gray-700`}>
            {isSidebarOpen ? (
              <h1 className="text-xl font-bold text-primary-600 dark:text-primary-400">{APP_NAME}</h1>
            ) : (
              <h1 className="text-xl font-bold text-primary-600 dark:text-primary-400">SMS</h1>
            )}
            <button
              onClick={toggleMobileMenu}
              className="p-1 rounded-md lg:hidden"
            >
              <Icons.Close />
            </button>
          </div>

          {/* Navigation */}
          <nav className="flex-1 px-2 py-4 space-y-2 overflow-y-auto">
            {getNavItems().map((item) => (
              <NavLink
                key={item.to}
                to={item.to}
                label={item.label}
                icon={item.icon}
                isSidebarOpen={isSidebarOpen}
                onClick={closeMobileMenu}
              />
            ))}
          </nav>

          {/* Bottom actions */}
          <div className="p-4 border-t border-gray-200 dark:border-gray-700">
            <button
              onClick={toggleDarkMode}
              className={`flex items-center p-2 w-full rounded-lg text-gray-600 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-800 ${isSidebarOpen ? '' : 'justify-center'}`}
            >
              <span className="flex-shrink-0">{darkMode ? <Icons.Sun /> : <Icons.Moon />}</span>
              {isSidebarOpen && <span className="ml-3">{darkMode ? 'Light Mode' : 'Dark Mode'}</span>}
            </button>
            <button
              onClick={handleLogout}
              className={`flex items-center p-2 w-full rounded-lg text-gray-600 hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-800 ${isSidebarOpen ? '' : 'justify-center'}`}
            >
              <span className="flex-shrink-0"><Icons.Logout /></span>
              {isSidebarOpen && <span className="ml-3">Logout</span>}
            </button>
          </div>
        </div>
      </aside>

      {/* Overlay */}
      {isMobileMenuOpen && (
        <div
          className="fixed inset-0 z-10 bg-gray-900 bg-opacity-50 lg:hidden"
          onClick={toggleMobileMenu}
        ></div>
      )}

      {/* Main Content */}
      <div className="flex flex-col flex-1 overflow-hidden">
        {/* Top Navigation */}
        <header className="flex items-center justify-between h-16 px-6 bg-white dark:bg-gray-800 shadow-sm z-10">
          <div className="flex items-center">
            <button
              onClick={toggleSidebar}
              className="p-1 mr-4 rounded-md focus:outline-none hidden lg:block"
            >
              <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-gray-600 dark:text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h7" />
              </svg>
            </button>
            <button
              onClick={toggleMobileMenu}
              className="p-1 mr-4 rounded-md focus:outline-none lg:hidden"
            >
              <Icons.Menu />
            </button>
          </div>

          {/* User Info */}
          <div className="flex items-center">
            <div className="mr-2 text-right hidden sm:block">
              <p className="text-sm font-medium text-gray-700 dark:text-gray-300">{user?.name}</p>
              <p className="text-xs text-gray-500 dark:text-gray-400">{user?.role}</p>
            </div>
            <div className="h-9 w-9 rounded-full bg-primary-100 dark:bg-primary-900 flex items-center justify-center text-primary-700 dark:text-primary-300 font-semibold">
              {user?.name?.charAt(0)}
            </div>
          </div>
        </header>

        {/* Page Content */}
        <main className="flex-1 overflow-y-auto p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default DashboardLayout; 