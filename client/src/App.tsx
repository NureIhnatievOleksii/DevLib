import AppLayout from './layouts/AppLayout/AppLayout';
import AppRouter from './app/router/AppRouter';
import './App.css';

function App() {
  return (
   /*  <AuthGuard> */
    <AppLayout>
      <AppRouter />
    </AppLayout>
 /*  </AuthGuard> */
  );
}

export default App;
