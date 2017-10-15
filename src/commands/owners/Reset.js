const db = require('../../database');
const patron = require('patron.js');

class Reset extends patron.Command {
  constructor() {
    super({
      names: ['reset'],
      groupName: 'owners',
      description: 'Resets all user data in your server.'
    });
  }

  async run(msg, args) {
    await msg.createReply('Are you sure you wish to reset all DEA related data within your server? Reply with "yes" to continue.');

    const filter = (x) => x.content.toLowerCase() === 'yes' && x.author.id === msg.author.id;
    const result = await msg.channel.awaitMessages(filter, { max: 1, time: 30000 });

    if (result.size === 1) {
      await db.userRepo.deleteUsers(msg.guild.id);
      return msg.createReply('You have successfully reset all data in your server.');
    }
  }
}

module.exports = new Reset();
