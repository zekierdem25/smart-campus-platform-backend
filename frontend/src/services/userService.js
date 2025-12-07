import api from './api';

const getDepartments = async () => {
    const response = await api.get('/users/departments');
    // API returns ApiResponseDto<List<DepartmentResponseDto>>
    // We need to return the Data part if success is true
    if (response.data.success) {
        return response.data.data;
    }
    return [];
};

const getCurrentUserProfile = async () => {
    const response = await api.get('/users/me');
    return response.data;
};

const updateProfile = async (profileData) => {
    const response = await api.put('/users/me', profileData);
    return response.data;
};

const updateProfilePicture = async (file) => {
    const formData = new FormData();
    formData.append('file', file);
    const response = await api.post('/users/me/profile-picture', formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
    return response.data;
};

const changePassword = async (passwordData) => {
    const response = await api.post('/users/me/change-password', passwordData);
    return response.data;
};

const userService = {
    getDepartments,
    getCurrentUserProfile,
    updateProfile,
    updateProfilePicture,
    changePassword,
};

export default userService;
