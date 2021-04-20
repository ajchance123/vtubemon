using VTubeMon.Data.Objects;

namespace VTubeMon.Data.Commands
{
    public class SelectUsersCommand : QueryCommand<User>
    {
        public SelectUsersCommand() : base ("users")
        {

        }
    }
}
