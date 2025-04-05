import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';

// Translation resources
const resources = {
  en: {
    translation: {
      // Common
      'app.name': 'School Management System',
      'loading': 'Loading...',
      'save': 'Save',
      'cancel': 'Cancel',
      'delete': 'Delete',
      'edit': 'Edit',
      'view': 'View',
      'search': 'Search',
      'actions': 'Actions',
      'confirm': 'Confirm',
      
      // Auth
      'login.title': 'Login to your account',
      'login.email': 'Email',
      'login.password': 'Password',
      'login.forgot_password': 'Forgot password?',
      'login.submit': 'Log in',
      'login.no_account': 'Don\'t have an account?',
      'login.register': 'Register',
      
      'register.title': 'Create an account',
      'register.name': 'Full Name',
      'register.email': 'Email',
      'register.password': 'Password',
      'register.confirm_password': 'Confirm Password',
      'register.role': 'Role',
      'register.submit': 'Register',
      'register.have_account': 'Already have an account?',
      'register.login': 'Log in',
      
      'logout': 'Logout',
      
      // Dashboard
      'dashboard.title': 'Dashboard',
      'dashboard.welcome': 'Welcome back, {{name}}!',
      'dashboard.stats.students': 'Total Students',
      'dashboard.stats.teachers': 'Total Teachers',
      'dashboard.stats.parents': 'Total Parents',
      'dashboard.stats.classes': 'Total Classes',
      'dashboard.students_by_class': 'Students by Class',
      'dashboard.attendance_trends': 'Attendance Trends',
      'dashboard.school_overview': 'School Overview',
      'dashboard.announcements': 'Recent Announcements',
      
      // Navigation
      'nav.dashboard': 'Dashboard',
      'nav.students': 'Students',
      'nav.teachers': 'Teachers',
      'nav.parents': 'Parents',
      'nav.classes': 'Classes',
      'nav.profile': 'Profile',
      'nav.dark_mode': 'Dark Mode',
      'nav.light_mode': 'Light Mode',
      
      // Students
      'students.title': 'Students',
      'students.add': 'Add New Student',
      'students.search': 'Search by name, roll number or admission number',
      'students.all_classes': 'All Classes',
      'students.all_sections': 'All Sections',
      'students.clear_filters': 'Clear Filters',
      'students.name': 'Name',
      'students.roll_number': 'Roll Number',
      'students.admission_number': 'Admission Number',
      'students.class': 'Class',
      'students.section': 'Section',
      'students.gender': 'Gender',
      'students.parent': 'Parent',
      'students.none_found': 'No students found',
      'students.show': 'Show',
      
      // Teachers
      'teachers.title': 'Teachers',
      
      // Parents
      'parents.title': 'Parents',
      
      // Classes
      'classes.title': 'Classes',
      
      // Profile
      'profile.title': 'My Profile',
      
      // Error pages
      '404.title': '404',
      '404.subtitle': 'Page Not Found',
      '404.message': 'Sorry, we couldn\'t find the page you\'re looking for. It might have been removed, had its name changed, or is temporarily unavailable.',
      '404.home': 'Go to Homepage',
      '404.dashboard': 'Go to Dashboard'
    }
  },
  es: {
    translation: {
      // Common
      'app.name': 'Sistema de Gestión Escolar',
      'loading': 'Cargando...',
      'save': 'Guardar',
      'cancel': 'Cancelar',
      'delete': 'Eliminar',
      'edit': 'Editar',
      'view': 'Ver',
      'search': 'Buscar',
      'actions': 'Acciones',
      'confirm': 'Confirmar',
      
      // Auth
      'login.title': 'Iniciar sesión en su cuenta',
      'login.email': 'Correo electrónico',
      'login.password': 'Contraseña',
      'login.forgot_password': '¿Olvidó su contraseña?',
      'login.submit': 'Iniciar sesión',
      'login.no_account': '¿No tiene una cuenta?',
      'login.register': 'Registrarse',
      
      'register.title': 'Crear una cuenta',
      'register.name': 'Nombre completo',
      'register.email': 'Correo electrónico',
      'register.password': 'Contraseña',
      'register.confirm_password': 'Confirmar contraseña',
      'register.role': 'Rol',
      'register.submit': 'Registrarse',
      'register.have_account': '¿Ya tiene una cuenta?',
      'register.login': 'Iniciar sesión',
      
      'logout': 'Cerrar sesión',
      
      // Dashboard
      'dashboard.title': 'Panel de control',
      'dashboard.welcome': '¡Bienvenido de nuevo, {{name}}!',
      'dashboard.stats.students': 'Total de estudiantes',
      'dashboard.stats.teachers': 'Total de profesores',
      'dashboard.stats.parents': 'Total de padres',
      'dashboard.stats.classes': 'Total de clases',
      'dashboard.students_by_class': 'Estudiantes por clase',
      'dashboard.attendance_trends': 'Tendencias de asistencia',
      'dashboard.school_overview': 'Vista general de la escuela',
      'dashboard.announcements': 'Anuncios recientes',
      
      // Navigation
      'nav.dashboard': 'Panel de control',
      'nav.students': 'Estudiantes',
      'nav.teachers': 'Profesores',
      'nav.parents': 'Padres',
      'nav.classes': 'Clases',
      'nav.profile': 'Perfil',
      'nav.dark_mode': 'Modo oscuro',
      'nav.light_mode': 'Modo claro',
      
      // Students
      'students.title': 'Estudiantes',
      'students.add': 'Agregar nuevo estudiante',
      'students.search': 'Buscar por nombre, número de lista o número de admisión',
      'students.all_classes': 'Todas las clases',
      'students.all_sections': 'Todas las secciones',
      'students.clear_filters': 'Limpiar filtros',
      'students.name': 'Nombre',
      'students.roll_number': 'Número de lista',
      'students.admission_number': 'Número de admisión',
      'students.class': 'Clase',
      'students.section': 'Sección',
      'students.gender': 'Género',
      'students.parent': 'Padre',
      'students.none_found': 'No se encontraron estudiantes',
      'students.show': 'Mostrar',
      
      // Teachers
      'teachers.title': 'Profesores',
      
      // Parents
      'parents.title': 'Padres',
      
      // Classes
      'classes.title': 'Clases',
      
      // Profile
      'profile.title': 'Mi perfil',
      
      // Error pages
      '404.title': '404',
      '404.subtitle': 'Página no encontrada',
      '404.message': 'Lo sentimos, no pudimos encontrar la página que está buscando. Es posible que haya sido eliminada, haya cambiado de nombre o no esté disponible temporalmente.',
      '404.home': 'Ir a la página de inicio',
      '404.dashboard': 'Ir al panel de control'
    }
  }
};

i18n
  // detect user language
  .use(LanguageDetector)
  // pass the i18n instance to react-i18next
  .use(initReactI18next)
  // init i18next
  .init({
    resources,
    fallbackLng: 'en',
    debug: process.env.NODE_ENV === 'development',
    interpolation: {
      escapeValue: false, // not needed for react as it escapes by default
    },
    react: {
      useSuspense: false,
    }
  });

export default i18n; 