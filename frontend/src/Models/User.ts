export type UserProfileToken = {
    userName: string;
    email: string;
    token: string;
};

export type UserProfile = {
    userName: string;
    email: string;
};

export type UserGet = {
    id: string;
    userName: string;
    profileImageUrl: string;
};

export type EditUserProfile = {
    profileImageUrl: string;
}