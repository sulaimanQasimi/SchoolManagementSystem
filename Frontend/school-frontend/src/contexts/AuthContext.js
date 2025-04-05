import React, { createContext, useContext, useState, useEffect } from 'react';
import axios from 'axios';
import { API_URL } from '../config/constants';

const AuthContext = createContext(null);

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem('token'));
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);

  // Configure axios defaults
  useEffect(() => {
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    } else {
      delete axios.defaults.headers.common['Authorization'];
    }
  }, [token]);

  // Check if user is authenticated on mount
  useEffect(() => {
    const verifyToken = async () => {
      if (!token) {
        setIsLoading(false);
        return;
      }

      try {
        const response = await axios.get(`${API_URL}/api/auth/verify`);
        setUser(response.data.data);
        setIsAuthenticated(true);
      } catch (err) {
        console.error('Token verification failed:', err);
        localStorage.removeItem('token');
        setToken(null);
        setUser(null);
        setIsAuthenticated(false);
      } finally {
        setIsLoading(false);
      }
    };

    verifyToken();
  }, [token]);

  // Login function
  const login = async (email, password) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await axios.post(`${API_URL}/api/auth/login`, { email, password });
      const { token, user } = response.data.data;
      
      localStorage.setItem('token', token);
      setToken(token);
      setUser(user);
      setIsAuthenticated(true);
      
      return response.data;
    } catch (err) {
      const errorMsg = err.response?.data?.message || 'An error occurred during login';
      setError(errorMsg);
      throw new Error(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  // Register function
  const register = async (userData) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await axios.post(`${API_URL}/api/auth/register`, userData);
      return response.data;
    } catch (err) {
      const errorMsg = err.response?.data?.message || 'An error occurred during registration';
      setError(errorMsg);
      throw new Error(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  // Logout function
  const logout = () => {
    localStorage.removeItem('token');
    setToken(null);
    setUser(null);
    setIsAuthenticated(false);
  };

  // Update user profile
  const updateProfile = async (userData) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await axios.put(`${API_URL}/api/users/profile`, userData);
      setUser(response.data.data);
      return response.data;
    } catch (err) {
      const errorMsg = err.response?.data?.message || 'An error occurred while updating profile';
      setError(errorMsg);
      throw new Error(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  // Reset password request
  const forgotPassword = async (email) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await axios.post(`${API_URL}/api/auth/forgot-password`, { email });
      return response.data;
    } catch (err) {
      const errorMsg = err.response?.data?.message || 'An error occurred while processing your request';
      setError(errorMsg);
      throw new Error(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  // Reset password with token
  const resetPassword = async (token, password) => {
    setError(null);
    setIsLoading(true);

    try {
      const response = await axios.post(`${API_URL}/api/auth/reset-password`, { token, password });
      return response.data;
    } catch (err) {
      const errorMsg = err.response?.data?.message || 'An error occurred while resetting your password';
      setError(errorMsg);
      throw new Error(errorMsg);
    } finally {
      setIsLoading(false);
    }
  };

  const value = {
    user,
    token,
    isAuthenticated,
    isLoading,
    error,
    login,
    register,
    logout,
    updateProfile,
    forgotPassword,
    resetPassword
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}; 