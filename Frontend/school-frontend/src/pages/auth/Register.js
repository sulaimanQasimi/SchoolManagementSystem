import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { ROLES } from '../../config/constants';

const Register = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: '',
    role: 'Student'
  });
  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [successMessage, setSuccessMessage] = useState('');
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    // Clear error when user starts typing
    if (errors[name]) {
      setErrors({ ...errors, [name]: '' });
    }
  };

  const validate = () => {
    const newErrors = {};
    
    if (!formData.name.trim()) {
      newErrors.name = 'Name is required';
    }
    
    if (!formData.email) {
      newErrors.email = 'Email is required';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'Email is invalid';
    }
    
    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }
    
    if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validate()) {
      return;
    }
    
    setIsSubmitting(true);
    try {
      const { confirmPassword, ...registerData } = formData;
      await register(registerData);
      setSuccessMessage('Registration successful! You can now log in.');
      setTimeout(() => {
        navigate('/login');
      }, 2000);
    } catch (err) {
      setErrors({ general: err.message });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="p-6">
      <h2 className="text-2xl font-bold text-center text-gray-800 dark:text-white mb-6">Create an account</h2>
      
      {successMessage && (
        <div className="bg-success-50 border border-success-300 text-success-800 dark:bg-success-900/20 dark:border-success-800 dark:text-success-300 px-4 py-3 rounded mb-4">
          {successMessage}
        </div>
      )}
      
      {errors.general && (
        <div className="bg-danger-50 border border-danger-300 text-danger-800 dark:bg-danger-900/20 dark:border-danger-800 dark:text-danger-300 px-4 py-3 rounded mb-4">
          {errors.general}
        </div>
      )}
      
      <form onSubmit={handleSubmit}>
        <div className="mb-4">
          <label htmlFor="name" className="form-label">Full Name</label>
          <input
            type="text"
            id="name"
            name="name"
            value={formData.name}
            onChange={handleChange}
            className={`form-input ${errors.name ? 'border-danger-500' : ''}`}
            placeholder="Enter your full name"
          />
          {errors.name && <p className="mt-1 text-sm text-danger-600 dark:text-danger-400">{errors.name}</p>}
        </div>
        
        <div className="mb-4">
          <label htmlFor="email" className="form-label">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            className={`form-input ${errors.email ? 'border-danger-500' : ''}`}
            placeholder="Enter your email"
          />
          {errors.email && <p className="mt-1 text-sm text-danger-600 dark:text-danger-400">{errors.email}</p>}
        </div>
        
        <div className="mb-4">
          <label htmlFor="role" className="form-label">Role</label>
          <select
            id="role"
            name="role"
            value={formData.role}
            onChange={handleChange}
            className="form-input"
          >
            <option value={ROLES.STUDENT}>{ROLES.STUDENT}</option>
            <option value={ROLES.PARENT}>{ROLES.PARENT}</option>
            <option value={ROLES.TEACHER}>{ROLES.TEACHER}</option>
          </select>
        </div>
        
        <div className="mb-4">
          <label htmlFor="password" className="form-label">Password</label>
          <input
            type="password"
            id="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            className={`form-input ${errors.password ? 'border-danger-500' : ''}`}
            placeholder="Enter your password"
          />
          {errors.password && <p className="mt-1 text-sm text-danger-600 dark:text-danger-400">{errors.password}</p>}
        </div>
        
        <div className="mb-6">
          <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
          <input
            type="password"
            id="confirmPassword"
            name="confirmPassword"
            value={formData.confirmPassword}
            onChange={handleChange}
            className={`form-input ${errors.confirmPassword ? 'border-danger-500' : ''}`}
            placeholder="Confirm your password"
          />
          {errors.confirmPassword && <p className="mt-1 text-sm text-danger-600 dark:text-danger-400">{errors.confirmPassword}</p>}
        </div>
        
        <button
          type="submit"
          disabled={isSubmitting}
          className="btn btn-primary w-full"
        >
          {isSubmitting ? (
            <span className="flex items-center justify-center">
              <svg className="animate-spin -ml-1 mr-2 h-4 w-4 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
              </svg>
              Registering...
            </span>
          ) : (
            'Register'
          )}
        </button>
      </form>
      
      <div className="text-center mt-6">
        <p className="text-sm text-gray-600 dark:text-gray-400">
          Already have an account?{' '}
          <Link to="/login" className="text-primary-600 hover:text-primary-500 dark:text-primary-400">
            Log in
          </Link>
        </p>
      </div>
    </div>
  );
};

export default Register; 