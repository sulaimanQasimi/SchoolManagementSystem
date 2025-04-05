import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuth } from '../contexts/AuthContext';
import { API_URL, CHART_COLORS } from '../config/constants';
import { Chart as ChartJS, ArcElement, CategoryScale, LinearScale, PointElement, LineElement, BarElement, Title, Tooltip, Legend } from 'chart.js';
import { Pie, Line, Bar } from 'react-chartjs-2';

// Register ChartJS components
ChartJS.register(ArcElement, CategoryScale, LinearScale, PointElement, LineElement, BarElement, Title, Tooltip, Legend);

const Dashboard = () => {
  const { user } = useAuth();
  const [isLoading, setIsLoading] = useState(true);
  const [stats, setStats] = useState({
    totalStudents: 0,
    totalTeachers: 0,
    totalParents: 0,
    totalClasses: 0,
    studentsByClass: {},
    attendanceByMonth: {},
    recentAnnouncements: []
  });

  useEffect(() => {
    const fetchDashboardData = async () => {
      setIsLoading(true);
      try {
        // In a real application, you would fetch actual data from the API
        // For now, we'll simulate the data
        // const response = await axios.get(`${API_URL}/api/dashboard/stats`);
        // setStats(response.data.data);

        // Simulated data
        setTimeout(() => {
          setStats({
            totalStudents: 248,
            totalTeachers: 22,
            totalParents: 198,
            totalClasses: 12,
            studentsByClass: {
              'Class 1': 25,
              'Class 2': 28,
              'Class 3': 22,
              'Class 4': 21,
              'Class 5': 24,
              'Class 6': 20,
              'Class 7': 22,
              'Class 8': 19,
              'Class 9': 30,
              'Class 10': 25,
              'Class 11': 18,
              'Class 12': 14
            },
            attendanceByMonth: {
              'Jan': 95,
              'Feb': 92,
              'Mar': 94,
              'Apr': 90,
              'May': 88,
              'Jun': 92,
              'Jul': 93,
              'Aug': 94,
              'Sep': 91,
              'Oct': 89,
              'Nov': 92,
              'Dec': 90
            },
            recentAnnouncements: [
              { id: 1, title: 'School holiday on Republic Day', date: '2023-01-24', author: 'Principal' },
              { id: 2, title: 'Annual Sports Day Postponed', date: '2023-01-18', author: 'Sports Teacher' },
              { id: 3, title: 'Parent-Teacher Meeting Schedule', date: '2023-01-15', author: 'Admin Office' }
            ]
          });
          setIsLoading(false);
        }, 1000);
      } catch (err) {
        console.error('Error fetching dashboard data:', err);
        setIsLoading(false);
      }
    };

    fetchDashboardData();
  }, []);

  // Prepare chart data
  const pieChartData = {
    labels: Object.keys(stats.studentsByClass),
    datasets: [
      {
        data: Object.values(stats.studentsByClass),
        backgroundColor: [
          CHART_COLORS.primary,
          CHART_COLORS.secondary,
          CHART_COLORS.success,
          CHART_COLORS.danger,
          CHART_COLORS.warning,
          CHART_COLORS.info,
          '#6366F1',
          '#8B5CF6',
          '#EC4899',
          '#14B8A6',
          '#F97316',
          '#A3E635'
        ],
        borderWidth: 1
      }
    ]
  };

  const lineChartData = {
    labels: Object.keys(stats.attendanceByMonth),
    datasets: [
      {
        label: 'Attendance %',
        data: Object.values(stats.attendanceByMonth),
        fill: false,
        backgroundColor: CHART_COLORS.primary,
        borderColor: CHART_COLORS.primary,
        tension: 0.4
      }
    ]
  };

  const lineChartOptions = {
    responsive: true,
    scales: {
      y: {
        min: 80,
        max: 100
      }
    },
    plugins: {
      legend: {
        position: 'top'
      },
      title: {
        display: true,
        text: 'Monthly Attendance Rate (%)'
      }
    }
  };

  const barChartData = {
    labels: ['Students', 'Teachers', 'Parents', 'Classes'],
    datasets: [
      {
        label: 'Total Count',
        data: [stats.totalStudents, stats.totalTeachers, stats.totalParents, stats.totalClasses],
        backgroundColor: [
          CHART_COLORS.primary,
          CHART_COLORS.secondary,
          CHART_COLORS.success,
          CHART_COLORS.warning
        ]
      }
    ]
  };

  const barChartOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top'
      },
      title: {
        display: true,
        text: 'School Statistics'
      }
    }
  };

  // Card component for stats
  const StatCard = ({ title, value, icon, color }) => (
    <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-5">
      <div className="flex justify-between">
        <div>
          <h3 className="text-sm font-medium text-gray-500 dark:text-gray-400">{title}</h3>
          <p className="text-2xl font-bold mt-1">{value}</p>
        </div>
        <div className={`h-12 w-12 rounded-full flex items-center justify-center ${color}`}>
          {icon}
        </div>
      </div>
    </div>
  );

  // Loading state
  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-full">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary-600"></div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold">Dashboard</h1>
        <p className="text-gray-600 dark:text-gray-400">Welcome back, {user?.name}!</p>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <StatCard
          title="Total Students"
          value={stats.totalStudents}
          icon={
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
            </svg>
          }
          color="bg-primary-500 text-white"
        />
        <StatCard
          title="Total Teachers"
          value={stats.totalTeachers}
          icon={
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
            </svg>
          }
          color="bg-secondary-500 text-white"
        />
        <StatCard
          title="Total Parents"
          value={stats.totalParents}
          icon={
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
            </svg>
          }
          color="bg-success-500 text-white"
        />
        <StatCard
          title="Total Classes"
          value={stats.totalClasses}
          icon={
            <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6 text-white" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
            </svg>
          }
          color="bg-warning-500 text-white"
        />
      </div>

      {/* Charts */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-5">
          <h2 className="text-lg font-semibold mb-4">Students by Class</h2>
          <div className="h-80">
            <Pie data={pieChartData} />
          </div>
        </div>
        <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-5">
          <h2 className="text-lg font-semibold mb-4">Attendance Trends</h2>
          <div className="h-80">
            <Line options={lineChartOptions} data={lineChartData} />
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-5 lg:col-span-1">
          <h2 className="text-lg font-semibold mb-4">School Overview</h2>
          <div className="h-80">
            <Bar options={barChartOptions} data={barChartData} />
          </div>
        </div>

        {/* Recent Announcements */}
        <div className="bg-white dark:bg-gray-800 rounded-lg shadow-md p-5 lg:col-span-2">
          <h2 className="text-lg font-semibold mb-4">Recent Announcements</h2>
          <div className="space-y-4">
            {stats.recentAnnouncements.map(announcement => (
              <div key={announcement.id} className="border-b border-gray-200 dark:border-gray-700 pb-4 last:border-b-0 last:pb-0">
                <h3 className="font-medium">{announcement.title}</h3>
                <div className="flex justify-between text-sm text-gray-500 dark:text-gray-400 mt-1">
                  <span>By: {announcement.author}</span>
                  <span>{new Date(announcement.date).toLocaleDateString()}</span>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard; 