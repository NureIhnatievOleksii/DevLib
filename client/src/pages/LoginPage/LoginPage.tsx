import React, { useEffect, useState } from 'react'
import AppLayout from '../../layouts/AppLayout/AppLayout'
import styles from './LoginPage.module.css';
import { Link, useNavigate } from 'react-router-dom';
import { RouteNames } from '../../app/router';
import googleIcon from '../../assets/images/icons/google.png'
import gitIcon from '../../assets/images/icons/git.png'
import { useAuthStore } from '../../app/store/auth';
import MyInput from '../../UI/MyInput/MyInput';
import LoginService from './api/LoginService';
import { validateStringFields } from '../../helpers/checkStringFields';


const LoginPage = () => {
  /*   const [loginData, setLoginData] = useState<ILoginData>({
      email: '',
      password: ''
    }); */
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const setRole = useAuthStore(store => store.setRole)
  const login = useAuthStore(store => store.login)
  const loggedIn = useAuthStore(store => store.loggedIn)
  const setLoggedIn = useAuthStore(store => store.setLoggedIn)
  const navigate = useNavigate()

  const handelLogin = async () => {
    console.log(validateStringFields({email, password}))
    //TODO validation
    if (validateStringFields({email, password})){
      try {
        await login(email, password)
      } catch (e) {
        alert(e)
      }
    }
     


    if (email == 'user' || email == 'admin') {
      setRole(email);
      setLoggedIn(true);

    } else {
      alert('incorrectly entered data')
  }




  }

  useEffect(() => {
    if (loggedIn) {
      navigate('/')
    }
  }, [loggedIn])
  return (
    <div className={styles.main}>
      <div className={styles.title}>Вхід до акаунту</div>

      <div className={styles.inputRow}>

        <MyInput
          placeholder='Пошта'
          value={email} setValue={setEmail}
        />
        <MyInput
          placeholder='Пароль'
          value={password} setValue={setPassword}
        />
      </div>

      <div className={styles.textRow}>
        <div className={styles.text}>забули пароль ?</div>
        <Link to={RouteNames.REGISTER} className={styles.text}>Зарееструватися</Link>
      </div>

      <button className={styles.googleButton}>
        <img src={googleIcon} alt="Google icon" />
        <div className={styles.buttonText}>зайти через google</div>
      </button>
      <button className={styles.gitButton}>
        <img src={gitIcon} alt="Git icon" />
        <div className={styles.buttonText}>зайти через github</div>
      </button>
      <button className={styles.logiButton} onClick={handelLogin}>
        Увійти
      </button>
    </div>
  )
}

export default LoginPage
