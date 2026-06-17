namespace School.Application.Authorization;

[Flags]
public enum Permissions
{
    ReadCourses = 0 << 1,

    CreateCourses = 1 << 1,
    EditCourses = 2 << 1,
    DeleteCourses = 3 << 1,

    CreateModules = 4 << 1,
    EditModules = 5 << 1,
    DeleteModules = 6 << 1,
    CreateLessons = 7 << 1,
    EditLessons = 8 << 1,
    DeleteLessons = 9 << 1,

    ViewUsers = 10 << 1,
    BlockUsers = 11 << 1,
    ChangeUserRoles = 12 << 1,

    ViewStudents = 13 << 1,
}
