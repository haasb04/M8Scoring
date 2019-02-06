using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Internal {
	internal class AuthRoles {
		private AuthRoles(string value) { Value = value; }

		public string Value { get; set; }

		public static AuthRoles	Admin { get { return new AuthRoles("Administrator"); } }
		public static AuthRoles RegisteredUser { get { return new AuthRoles("RegisteredUser"); } }
	}
}
