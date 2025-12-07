import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import userService from '../services/userService';
import { useAuth } from '../context/AuthContext';
import Navbar from '../components/Navbar';
import Alert from '../components/Alert';
import FormInput from '../components/FormInput';

// Validation schema for profile update
const profileSchema = yup.object().shape({
    firstName: yup.string().required('Ad zorunludur'),
    lastName: yup.string().required('Soyad zorunludur'),
    phone: yup.string().nullable(),
});

// Validation schema for password change
const passwordSchema = yup.object().shape({
    currentPassword: yup.string().required('Mevcut şifre zorunludur'),
    newPassword: yup.string()
        .required('Yeni şifre zorunludur')
        .min(8, 'Şifre en az 8 karakter olmalıdır')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/, 'Şifre en az 8 karakter, büyük/küçük harf ve rakam içermelidir'),
    confirmPassword: yup.string()
        .oneOf([yup.ref('newPassword'), null], 'Şifreler eşleşmiyor')
        .required('Şifre tekrarı zorunludur'),
});

const Profile = () => {
    const { user, updateUser } = useAuth();
    const [isEditing, setIsEditing] = useState(false);
    const [message, setMessage] = useState(null); // { type: 'success' | 'error', text: string }

    // Profile Form
    const { register: registerProfile, handleSubmit: handleProfileSubmit, formState: { errors: profileErrors }, reset: resetProfile } = useForm({
        resolver: yupResolver(profileSchema),
    });

    // Password Form
    const { register: registerPassword, handleSubmit: handlePasswordSubmit, formState: { errors: passwordErrors }, reset: resetPassword } = useForm({
        resolver: yupResolver(passwordSchema),
    });

    useEffect(() => {
        if (user) {
            resetProfile({
                firstName: user.firstName,
                lastName: user.lastName,
                phone: user.phone
            });
        }
    }, [user, resetProfile]);

    const onProfileUpdate = async (data) => {
        try {
            const response = await userService.updateProfile(data);
            if (response.success) {
                updateUser(response.data);
                setIsEditing(false);
                setMessage({ type: 'success', text: 'Profil başarıyla güncellendi.' });
            }
        } catch (error) {
            const errorMsg = error.response?.data?.message || 'Profil güncellenirken bir hata oluştu.';
            setMessage({ type: 'error', text: errorMsg });
        }
    };

    const onPasswordChange = async (data) => {
        try {
            const response = await userService.changePassword(data);
            if (response.success) {
                setMessage({ type: 'success', text: 'Şifreniz başarıyla değiştirildi.' });
                resetPassword();
            }
        } catch (error) {
            const errorMsg = error.response?.data?.message || 'Şifre değiştirilirken bir hata oluştu.';
            setMessage({ type: 'error', text: errorMsg });
        }
    };

    const handleFileChange = async (event) => {
        const file = event.target.files[0];
        if (!file) return;

        try {
            const response = await userService.updateProfilePicture(file);
            if (response.success) {
                // Update specific field in user context without full reload if possible,
                // otherwise we assume response.data contains the new URL string
                updateUser({ profilePictureUrl: response.data });
                setMessage({ type: 'success', text: 'Profil fotoğrafı güncellendi.' });
            }
        } catch (error) {
            const errorMsg = error.response?.data?.message || 'Fotoğraf yüklenirken hata oluştu.';
            setMessage({ type: 'error', text: errorMsg });
        }
    };

    if (!user) return <div className="p-8 text-center">Yükleniyor...</div>;

    return (
        <div className="min-h-screen bg-gray-100">
            <Navbar />
            <div className="py-10">
                <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
                    <h1 className="text-3xl font-bold text-gray-900 mb-8">Profil Ayarları</h1>

                    <Alert type={message?.type} message={message?.text} />

                    <div className="bg-white shadow rounded-lg overflow-hidden mb-8 mt-4">
                        <div className="p-6">
                            <h2 className="text-xl font-semibold text-gray-900 mb-4">Kişisel Bilgiler</h2>

                            {!isEditing ? (
                                <div className="space-y-4">
                                    <div className="flex items-center space-x-4">
                                        <div className="relative h-20 w-20">
                                            <img
                                                className="h-20 w-20 rounded-full object-cover"
                                                src={user.profilePictureUrl ? `http://localhost:5000${user.profilePictureUrl}` : "https://via.placeholder.com/150"}
                                                alt="Profile"
                                                onError={(e) => { e.target.onerror = null; e.target.src = "https://via.placeholder.com/150"; }}
                                            />
                                            <label htmlFor="file-upload" className="absolute bottom-0 right-0 bg-white rounded-full p-1 shadow cursor-pointer hover:bg-gray-50">
                                                <svg className="h-4 w-4 text-gray-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 9a2 2 0 012-2h.93a2 2 0 001.664-.89l.812-1.22A2 2 0 0110.07 4h3.86a2 2 0 011.664.89l.812 1.22A2 2 0 0018.07 7H19a2 2 0 012 2v9a2 2 0 01-2 2H5a2 2 0 01-2-2V9z" />
                                                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 13a3 3 0 11-6 0 3 3 0 016 0z" />
                                                </svg>
                                                <input id="file-upload" type="file" className="hidden" onChange={handleFileChange} accept="image/*" />
                                            </label>
                                        </div>
                                        <div>
                                            <h3 className="text-lg font-medium text-gray-900">{user.fullName}</h3>
                                            <p className="text-sm text-gray-500">{user.email}</p>
                                            <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
                                                {user.role === 'Student' ? 'Öğrenci' : 'Akademisyen'}
                                            </span>
                                        </div>
                                    </div>

                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 border-t pt-4">
                                        <div>
                                            <span className="block text-sm font-medium text-gray-500">Ad Soyad</span>
                                            <span className="block text-sm text-gray-900">{user.firstName} {user.lastName}</span>
                                        </div>
                                        <div>
                                            <span className="block text-sm font-medium text-gray-500">Telefon</span>
                                            <span className="block text-sm text-gray-900">{user.phone || '-'}</span>
                                        </div>
                                    </div>

                                    <div className="flex justify-end">
                                        <button
                                            onClick={() => setIsEditing(true)}
                                            className="bg-campus-blue text-white px-4 py-2 rounded hover:bg-campus-blue-dark transition"
                                        >
                                            Düzenle
                                        </button>
                                    </div>
                                </div>
                            ) : (
                                <form onSubmit={handleProfileSubmit(onProfileUpdate)} className="space-y-4">
                                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                        <FormInput
                                            label="Ad"
                                            {...registerProfile('firstName')}
                                            error={profileErrors.firstName}
                                        />
                                        <FormInput
                                            label="Soyad"
                                            {...registerProfile('lastName')}
                                            error={profileErrors.lastName}
                                        />
                                        <FormInput
                                            label="Telefon"
                                            {...registerProfile('phone')}
                                            error={profileErrors.phone}
                                        />
                                    </div>
                                    <div className="flex justify-end space-x-2">
                                        <button
                                            type="button"
                                            onClick={() => setIsEditing(false)}
                                            className="bg-gray-200 text-gray-700 px-4 py-2 rounded hover:bg-gray-300 transition"
                                        >
                                            İptal
                                        </button>
                                        <button
                                            type="submit"
                                            className="bg-campus-green text-white px-4 py-2 rounded hover:bg-campus-green-dark transition"
                                        >
                                            Kaydet
                                        </button>
                                    </div>
                                </form>
                            )}
                        </div>
                    </div>

                    <div className="bg-white shadow rounded-lg overflow-hidden">
                        <div className="p-6 flex flex-col items-center">
                            <h2 className="text-xl font-semibold text-gray-900 mb-4">Şifre Değiştir</h2>
                            <form onSubmit={handlePasswordSubmit(onPasswordChange)} className="space-y-4 max-w-md">
                                <FormInput
                                    label="Mevcut Şifre"
                                    type="password"
                                    {...registerPassword('currentPassword')}
                                    error={passwordErrors.currentPassword}
                                />
                                <FormInput
                                    label="Yeni Şifre"
                                    type="password"
                                    {...registerPassword('newPassword')}
                                    error={passwordErrors.newPassword}
                                />
                                <FormInput
                                    label="Yeni Şifre Tekrar"
                                    type="password"
                                    {...registerPassword('confirmPassword')}
                                    error={passwordErrors.confirmPassword}
                                />
                                <div>
                                    <button
                                        type="submit"
                                        className="bg-campus-blue text-white px-4 py-2 rounded hover:bg-campus-blue-dark transition"
                                    >
                                        Şifreyi Güncelle
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Profile;
