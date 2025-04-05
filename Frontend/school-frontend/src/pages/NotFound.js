import React from 'react';
import { Link } from 'react-router-dom';

const NotFound = () => {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100 dark:bg-gray-900 px-4 py-12 text-center">
      <svg
        className="w-24 h-24 text-primary-600 dark:text-primary-400 mb-6"
        fill="none"
        stroke="currentColor"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          strokeWidth={2}
          d="M9.172 16.172a4 4 0 015.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
        />
      </svg>
      
      <h1 className="text-6xl font-extrabold text-gray-900 dark:text-white mb-4">404</h1>
      
      <h2 className="text-2xl font-semibold text-gray-700 dark:text-gray-300 mb-4">
        Page Not Found
      </h2>
      
      <p className="text-gray-600 dark:text-gray-400 max-w-md mb-8">
        Sorry, we couldn't find the page you're looking for. It might have been removed, had its name changed, or is temporarily unavailable.
      </p>
      
      <div className="flex flex-col sm:flex-row space-y-4 sm:space-y-0 sm:space-x-4">
        <Link
          to="/"
          className="btn btn-primary"
        >
          Go to Homepage
        </Link>
        
        <Link
          to="/dashboard"
          className="btn btn-outline border-primary-500 text-primary-600 hover:bg-primary-50 dark:border-primary-700 dark:text-primary-500 dark:hover:bg-gray-800"
        >
          Go to Dashboard
        </Link>
      </div>
    </div>
  );
};

export default NotFound; 