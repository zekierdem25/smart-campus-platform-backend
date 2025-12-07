import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import FormInput from '../components/FormInput';
import Alert from '../components/Alert';
import LoadingSpinner from '../components/LoadingSpinner';

const schema = yup.object().shape({
    email: yup.string().email('Geçerli bir email adresi giriniz').required('Email adresi zorunludur'),
    password: yup.string().required('Şifre zorunludur').min(6, 'Şifre en az 6 karakter olmalıdır'),
});

const Login = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [apiError, setApiError] = useState(null);
    const navigate = useNavigate();
    const { login } = useAuth();
    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(schema),
    });

    const onSubmit = async (data) => {
        setIsLoading(true);
        setApiError(null);
        try {
            const result = await login(data.email, data.password, data.rememberMe);
            if (result.success) {
                navigate('/dashboard');
            } else {
                setApiError(result.message || 'Giriş başarısız.');
            }
        } catch (error) {
            console.error('Login error:', error);
            if (error.response && error.response.data && error.response.data.message) {
                setApiError(error.response.data.message);
            } else {
                setApiError('Giriş başarısız. Lütfen bilgilerinizi kontrol ediniz.');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-xl shadow-lg">
                <div>
                    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
                        Smart Campus
                    </h2>
                    <p className="mt-2 text-center text-sm text-gray-600">
                        Öğrenci Giriş Sistemi
                    </p>
                    <p className="mt-2 text-center text-sm text-gray-600">
                        veya <Link to="/register" className="font-medium text-campus-blue hover:text-campus-blue-dark">kayıt ol</Link>
                    </p>
                </div>

                <Alert type="error" message={apiError} />

                <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
                    <div className="rounded-md shadow-sm -space-y-px">
                        <FormInput
                            id="email-address"
                            label="Email Adresi"
                            type="email"
                            autoComplete="email"
                            placeholder="Email Adresi"
                            error={errors.email}
                            {...register('email')}
                        />
                        <div className="mt-4">
                            <FormInput
                                id="password"
                                label="Şifre"
                                type="password"
                                autoComplete="current-password"
                                placeholder="Şifre"
                                error={errors.password}
                                {...register('password')}
                            />
                        </div>
                    </div>

                    <div className="flex items-center justify-between">
                        <div className="flex items-center">
                            <input
                                id="remember-me"
                                name="remember-me"
                                type="checkbox"
                                className="h-4 w-4 text-campus-green focus:ring-campus-green border-gray-300 rounded"
                                {...register('rememberMe')}
                            />
                            <label htmlFor="remember-me" className="ml-2 block text-sm text-gray-900">
                                Beni Hatırla
                            </label>
                        </div>

                        <div className="text-sm">
                            <Link to="/forgot-password" className="font-medium text-campus-blue hover:text-campus-blue-dark">
                                Şifremi Unuttum?
                            </Link>
                        </div>
                    </div>

                    <div>
                        <button
                            type="submit"
                            disabled={isLoading}
                            className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-campus-green hover:bg-campus-green-dark focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-campus-green disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-200"
                        >
                            {isLoading && <span className="mr-2"><LoadingSpinner size="sm" /></span>}
                            {isLoading ? 'Giriş Yapılıyor...' : 'Giriş Yap'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;
