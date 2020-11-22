namespace Helpers
{
    public class FirebaseSettingsRecipe
    {
        public const string DATABASE_URL = "https://firestore.googleapis.com/v1/projects/[YOUR-FIREBASE-PROJECT]databases/(default)/documents/lotteries";
        public const string DATABASE_URL_PARTIAL = "projects/[YOUR-FIREBASE-PROJECT]/databases/(default)/documents/lotteries";
       public static string idToken = "";
        public const string API_URL = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
        public const string API_KEY = "[YOUR-API-KEY]";
        public const string ADMIN_MAIL = "admin@vinlotteriet.no";
        public const string ADMIN_PW = " [ADMIN-PW]";
    }
}
