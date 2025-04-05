import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { API_URL, DEFAULT_PAGE_SIZE, PAGE_SIZE_OPTIONS } from '../../config/constants';
import { useAuth } from '../../contexts/AuthContext';

const Students = () => {
  const { user } = useAuth();
  const [students, setStudents] = useState([]);
  const [filteredStudents, setFilteredStudents] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [search, setSearch] = useState('');
  const [classFilter, setClassFilter] = useState('');
  const [sectionFilter, setSectionFilter] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE);
  const [totalStudents, setTotalStudents] = useState(0);
  const [totalPages, setTotalPages] = useState(0);
  const [classes, setClasses] = useState([]);
  const [sections, setSections] = useState([]);

  useEffect(() => {
    const fetchStudents = async () => {
      setIsLoading(true);
      setError(null);
      try {
        // For demo purposes, we'll simulate the API call
        // In a real app, you would make an actual API call like this:
        // const response = await axios.get(`${API_URL}/api/students`, {
        //   params: {
        //     page: currentPage,
        //     pageSize,
        //     search,
        //     classId: classFilter,
        //     sectionId: sectionFilter
        //   }
        // });
        // setStudents(response.data.data);
        // setTotalStudents(response.data.totalItems);
        // setTotalPages(response.data.totalPages);

        // Simulate API response with mock data
        setTimeout(() => {
          const mockStudents = [
            { id: 1, name: 'John Doe', rollNumber: 'A001', className: 'Class 10', section: 'A', parentName: 'Mike Doe', admissionNumber: '2020001', gender: 'Male' },
            { id: 2, name: 'Jane Smith', rollNumber: 'A002', className: 'Class 10', section: 'A', parentName: 'Sarah Smith', admissionNumber: '2020002', gender: 'Female' },
            { id: 3, name: 'Alex Johnson', rollNumber: 'A003', className: 'Class 10', section: 'B', parentName: 'Robert Johnson', admissionNumber: '2020003', gender: 'Male' },
            { id: 4, name: 'Emily Davis', rollNumber: 'A004', className: 'Class 9', section: 'A', parentName: 'Michael Davis', admissionNumber: '2020004', gender: 'Female' },
            { id: 5, name: 'Michael Brown', rollNumber: 'A005', className: 'Class 9', section: 'B', parentName: 'James Brown', admissionNumber: '2020005', gender: 'Male' },
            { id: 6, name: 'Sophia Wilson', rollNumber: 'A006', className: 'Class 8', section: 'A', parentName: 'Thomas Wilson', admissionNumber: '2020006', gender: 'Female' },
            { id: 7, name: 'William Martin', rollNumber: 'A007', className: 'Class 8', section: 'B', parentName: 'David Martin', admissionNumber: '2020007', gender: 'Male' },
            { id: 8, name: 'Olivia Thompson', rollNumber: 'A008', className: 'Class 7', section: 'A', parentName: 'Richard Thompson', admissionNumber: '2020008', gender: 'Female' },
            { id: 9, name: 'James Garcia', rollNumber: 'A009', className: 'Class 7', section: 'B', parentName: 'Joseph Garcia', admissionNumber: '2020009', gender: 'Male' },
            { id: 10, name: 'Emma Rodriguez', rollNumber: 'A010', className: 'Class 6', section: 'A', parentName: 'Daniel Rodriguez', admissionNumber: '2020010', gender: 'Female' },
            { id: 11, name: 'Alexander Lee', rollNumber: 'A011', className: 'Class 6', section: 'B', parentName: 'William Lee', admissionNumber: '2020011', gender: 'Male' },
            { id: 12, name: 'Mia Lewis', rollNumber: 'A012', className: 'Class 5', section: 'A', parentName: 'Charles Lewis', admissionNumber: '2020012', gender: 'Female' },
          ];

          const mockClasses = ['Class 5', 'Class 6', 'Class 7', 'Class 8', 'Class 9', 'Class 10'];
          const mockSections = ['A', 'B', 'C'];

          setStudents(mockStudents);
          setClasses(mockClasses);
          setSections(mockSections);
          setTotalStudents(mockStudents.length);
          
          setIsLoading(false);
        }, 1000);
      } catch (err) {
        setError('Failed to fetch students. Please try again later.');
        setIsLoading(false);
      }
    };

    fetchStudents();
  }, [currentPage, pageSize, classFilter, sectionFilter]);

  // Filter students based on search query and filters
  useEffect(() => {
    const filtered = students.filter(student => {
      const matchesSearch = !search || 
        student.name.toLowerCase().includes(search.toLowerCase()) ||
        student.rollNumber.toLowerCase().includes(search.toLowerCase()) ||
        student.admissionNumber.toLowerCase().includes(search.toLowerCase());

      const matchesClass = !classFilter || student.className === classFilter;
      const matchesSection = !sectionFilter || student.section === sectionFilter;

      return matchesSearch && matchesClass && matchesSection;
    });

    setFilteredStudents(filtered);
    setTotalPages(Math.ceil(filtered.length / pageSize));
  }, [students, search, classFilter, sectionFilter, pageSize]);

  // Get current page of students
  const getCurrentPageStudents = () => {
    const startIndex = (currentPage - 1) * pageSize;
    const endIndex = startIndex + pageSize;
    return filteredStudents.slice(startIndex, endIndex);
  };

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handlePageSizeChange = (e) => {
    setPageSize(Number(e.target.value));
    setCurrentPage(1); // Reset to first page when page size changes
  };

  const handleSearch = (e) => {
    setSearch(e.target.value);
    setCurrentPage(1); // Reset to first page when search changes
  };

  const handleClassFilter = (e) => {
    setClassFilter(e.target.value);
    setCurrentPage(1); // Reset to first page when filter changes
  };

  const handleSectionFilter = (e) => {
    setSectionFilter(e.target.value);
    setCurrentPage(1); // Reset to first page when filter changes
  };

  const handleClearFilters = () => {
    setSearch('');
    setClassFilter('');
    setSectionFilter('');
    setCurrentPage(1);
  };

  // Pagination component
  const Pagination = () => {
    const pageNumbers = [];
    
    for (let i = 1; i <= totalPages; i++) {
      pageNumbers.push(i);
    }

    return (
      <div className="flex items-center justify-between mt-6">
        <div className="text-sm text-gray-600 dark:text-gray-400">
          Showing {filteredStudents.length > 0 ? (currentPage - 1) * pageSize + 1 : 0} to {Math.min(currentPage * pageSize, filteredStudents.length)} of {filteredStudents.length} students
        </div>
        <div className="flex space-x-1">
          <button
            onClick={() => handlePageChange(currentPage - 1)}
            disabled={currentPage === 1}
            className={`px-3 py-1 rounded-md ${
              currentPage === 1
                ? 'bg-gray-100 text-gray-400 cursor-not-allowed dark:bg-gray-700 dark:text-gray-500'
                : 'bg-gray-200 text-gray-700 hover:bg-gray-300 dark:bg-gray-700 dark:text-gray-300 dark:hover:bg-gray-600'
            }`}
          >
            Previous
          </button>
          
          {pageNumbers.map(number => (
            <button
              key={number}
              onClick={() => handlePageChange(number)}
              className={`px-3 py-1 rounded-md ${
                currentPage === number
                  ? 'bg-primary-600 text-white'
                  : 'bg-gray-200 text-gray-700 hover:bg-gray-300 dark:bg-gray-700 dark:text-gray-300 dark:hover:bg-gray-600'
              }`}
            >
              {number}
            </button>
          ))}
          
          <button
            onClick={() => handlePageChange(currentPage + 1)}
            disabled={currentPage === totalPages || totalPages === 0}
            className={`px-3 py-1 rounded-md ${
              currentPage === totalPages || totalPages === 0
                ? 'bg-gray-100 text-gray-400 cursor-not-allowed dark:bg-gray-700 dark:text-gray-500'
                : 'bg-gray-200 text-gray-700 hover:bg-gray-300 dark:bg-gray-700 dark:text-gray-300 dark:hover:bg-gray-600'
            }`}
          >
            Next
          </button>
        </div>
      </div>
    );
  };

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary-600"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="bg-danger-50 border border-danger-300 text-danger-800 dark:bg-danger-900/20 dark:border-danger-800 dark:text-danger-300 px-4 py-3 rounded">
        {error}
      </div>
    );
  }

  return (
    <div>
      <div className="flex flex-col md:flex-row md:items-center md:justify-between mb-6">
        <h1 className="text-2xl font-bold">Students</h1>
        {user?.role === 'Admin' && (
          <Link to="/students/create" className="btn btn-primary mt-4 md:mt-0">
            <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 mr-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" />
            </svg>
            Add New Student
          </Link>
        )}
      </div>

      {/* Search and Filters */}
      <div className="bg-white dark:bg-gray-800 p-4 rounded-lg shadow-sm mb-6">
        <div className="flex flex-col md:flex-row md:items-center space-y-3 md:space-y-0 md:space-x-4">
          <div className="flex-1">
            <label htmlFor="search" className="sr-only">Search</label>
            <div className="relative">
              <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 text-gray-400" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                </svg>
              </div>
              <input
                type="text"
                id="search"
                placeholder="Search by name, roll number or admission number"
                value={search}
                onChange={handleSearch}
                className="form-input pl-10"
              />
            </div>
          </div>

          <div className="w-full md:w-40">
            <label htmlFor="class-filter" className="sr-only">Class</label>
            <select
              id="class-filter"
              value={classFilter}
              onChange={handleClassFilter}
              className="form-input"
            >
              <option value="">All Classes</option>
              {classes.map(cls => (
                <option key={cls} value={cls}>{cls}</option>
              ))}
            </select>
          </div>

          <div className="w-full md:w-40">
            <label htmlFor="section-filter" className="sr-only">Section</label>
            <select
              id="section-filter"
              value={sectionFilter}
              onChange={handleSectionFilter}
              className="form-input"
            >
              <option value="">All Sections</option>
              {sections.map(section => (
                <option key={section} value={section}>{section}</option>
              ))}
            </select>
          </div>

          <button
            onClick={handleClearFilters}
            className="btn btn-outline border-gray-300 text-gray-700 dark:border-gray-600 dark:text-gray-300"
          >
            Clear Filters
          </button>
        </div>
      </div>

      {/* Students Table */}
      <div className="bg-white dark:bg-gray-800 rounded-lg shadow-sm overflow-hidden">
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200 dark:divide-gray-700">
            <thead className="bg-gray-50 dark:bg-gray-700">
              <tr>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Name
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Roll Number
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Class
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Section
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Gender
                </th>
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Parent
                </th>
                <th scope="col" className="px-6 py-3 text-right text-xs font-medium text-gray-500 dark:text-gray-300 uppercase tracking-wider">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody className="bg-white dark:bg-gray-800 divide-y divide-gray-200 dark:divide-gray-700">
              {getCurrentPageStudents().length > 0 ? (
                getCurrentPageStudents().map(student => (
                  <tr key={student.id} className="hover:bg-gray-50 dark:hover:bg-gray-700">
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm font-medium text-gray-900 dark:text-white">{student.name}</div>
                      <div className="text-sm text-gray-500 dark:text-gray-400">{student.admissionNumber}</div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">
                      {student.rollNumber}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">
                      {student.className}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">
                      {student.section}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">
                      {student.gender}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 dark:text-gray-400">
                      {student.parentName}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                      <div className="flex justify-end space-x-2">
                        <Link
                          to={`/students/${student.id}`}
                          className="text-primary-600 hover:text-primary-900 dark:text-primary-400 dark:hover:text-primary-300"
                        >
                          View
                        </Link>
                        {user?.role === 'Admin' && (
                          <>
                            <Link
                              to={`/students/${student.id}/edit`}
                              className="text-secondary-600 hover:text-secondary-900 dark:text-secondary-400 dark:hover:text-secondary-300"
                            >
                              Edit
                            </Link>
                            <button
                              className="text-danger-600 hover:text-danger-900 dark:text-danger-400 dark:hover:text-danger-300"
                            >
                              Delete
                            </button>
                          </>
                        )}
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan="7" className="px-6 py-4 text-center text-gray-500 dark:text-gray-400">
                    No students found
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
        
        {/* Table Footer with Pagination */}
        <div className="bg-gray-50 dark:bg-gray-700 px-4 py-3 flex items-center justify-between">
          <div className="flex items-center">
            <span className="mr-2 text-sm text-gray-600 dark:text-gray-400">Show:</span>
            <select
              value={pageSize}
              onChange={handlePageSizeChange}
              className="form-input text-sm py-1 pl-2 pr-8"
            >
              {PAGE_SIZE_OPTIONS.map(size => (
                <option key={size} value={size}>{size}</option>
              ))}
            </select>
          </div>
          
          <Pagination />
        </div>
      </div>
    </div>
  );
};

export default Students; 